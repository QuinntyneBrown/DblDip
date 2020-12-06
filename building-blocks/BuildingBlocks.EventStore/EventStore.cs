using BuildingBlocks.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            Guid aggregateId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregateRoot, null);
            string aggregate = aggregateRoot.GetType().Name;

            foreach (var @event in aggregateRoot.DomainEvents)
            {
                _changes.Add(new StoredEvent()
                {
                    StoredEventId = Guid.NewGuid(),
                    Aggregate = aggregate,
                    AggregateDotNetType = type.AssemblyQualifiedName,
                    Data = SerializeObject(@event),
                    StreamId = aggregateId,
                    DotNetType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedOn = _dateTime.UtcNow,
                    Sequence = 0,
                    CorrelationId = _correlationIdAccessor.CorrelationId
                });
            }
            aggregateRoot.ClearChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var e in _changes)
            {
                _context.StoredEvents.Add(e);
            }

            var result = await _context.SaveChangesAsync(cancellationToken);

            _changes.Clear();

            return result;
        }


    }
}
