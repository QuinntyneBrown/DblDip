using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace BuildingBlocks.EventStore
{
    public class EventStore : IEventStore
    {
        private readonly List<StoredEvent> _changes = new List<StoredEvent>();
        private readonly IDateTime _dateTime;
        private readonly IEventStoreDbContext _context;
        private readonly ICorrelationIdAccessor _correlationIdAccessor;

        public EventStore(IEventStoreDbContext context, IDateTime dateTime, ICorrelationIdAccessor correlationIdAccessor)
        {
            _dateTime = dateTime;
            _context = context;
            _correlationIdAccessor = correlationIdAccessor;
        }

        public void Store(AggregateRoot aggregateRoot)
        {
            var type = aggregateRoot.GetType();

            _changes.AddRange(aggregateRoot.DomainEvents.Select(@event => new StoredEvent
            {
                StoredEventId = Guid.NewGuid(),
                Aggregate = aggregateRoot.GetType().Name,
                AggregateDotNetType = aggregateRoot.GetType().AssemblyQualifiedName,
                Data = SerializeObject(@event),
                StreamId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregateRoot, null),
                DotNetType = @event.GetType().AssemblyQualifiedName,
                Type = @event.GetType().Name,
                CreatedOn = _dateTime.UtcNow,
                Sequence = 0,
                CorrelationId = _correlationIdAccessor.CorrelationId
            }));

            aggregateRoot.ClearChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            _context.StoredEvents.AddRange(_changes);

            _changes.Clear();

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
