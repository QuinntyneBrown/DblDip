using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.Serialization.FormatterServices;

namespace DblDip.Core.Handlers
{
    public class EventStoreChangedHandler : INotificationHandler<EventStoreChanged>
    {
        private readonly IDblDipDbContext _context;
        private readonly Dictionary<Guid, IAggregateRoot> _inMemoryAggregates = new Dictionary<Guid, IAggregateRoot>();

        public EventStoreChangedHandler(IDblDipDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EventStoreChanged notification, CancellationToken cancellationToken)
        {
            foreach (var storedEvent in notification.Events)
            {
                var type = Type.GetType(storedEvent.AggregateDotNetType);

                var entity = await FindAsync(type, storedEvent.StreamId);
                
                entity.Apply(JsonConvert.DeserializeObject(storedEvent.Data, Type.GetType(storedEvent.DotNetType)) as IEvent);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<IAggregateRoot> FindAsync(Type type, Guid streamId)
        {

            var aggregate = _inMemoryAggregates.SingleOrDefault(x => x.Key == streamId).Value;

            bool addToInMemoryAggregates = false;

            if (aggregate == null)
            {
                aggregate = (await _context.FindAsync(type, streamId)) as IAggregateRoot;
                addToInMemoryAggregates = true;
            }

            if (aggregate == null)
            {
                aggregate = GetUninitializedObject(type) as IAggregateRoot;

                _ = _context.Add(aggregate);
            }

            if (addToInMemoryAggregates)
            {
                _inMemoryAggregates.Add(streamId, aggregate);
            }

            return aggregate;
        }
    }
}
