using BuildingBlocks.EventStore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace DblDip.Testing
{
    public class EventStoreBuilder
    {
        private EventStore _store;

        public static EventStore WithDefaults()
        {
            var options = new DbContextOptionsBuilder<EventStore>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options;

            var dateTime = new MachineDateTime();

            var correlationIdAccessor = new TestCorrelationIdAccessor(Guid.NewGuid());

            var eventStore = new EventStore(options, dateTime, correlationIdAccessor, Mock.Of<IMediator>());

            return eventStore;
        }

        public EventStoreBuilder()
        {
            _store = WithDefaults();
        }

        public EventStoreBuilder Add<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : AggregateRoot
        {
            _store.Add(aggregateRoot);
            return this;
        }

        public EventStoreBuilder SaveChanges()
        {
            _store.SaveChangesAsync(default).GetAwaiter().GetResult();

            return this;
        }

        public EventStore Build()
        {
            return _store;
        }
    }
}
