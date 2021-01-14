using BuildingBlocks.EventStore;
using Microsoft.EntityFrameworkCore;
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


            var eventStore = new EventStore(options, default, default, default);

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
