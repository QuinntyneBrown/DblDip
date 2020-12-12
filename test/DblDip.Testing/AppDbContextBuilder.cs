using BuildingBlocks.EventStore;
using Microsoft.EntityFrameworkCore;
using DblDip.Core.Models;
using System;
using DblDip.Data;
using DblDip.Testing.Factories;
using BuildingBlocks.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Testing
{
    public class AppDbContextBuilder
    {
        private AppDbContext _appDbContext;

        public static AppDbContext WithDefaults()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options;

            var context = new EventStoreDbContext(options);
            var dateTime = new MachineDateTime();
            var eventStore = new EventStore(context, dateTime, new TestCorrelationIdAccessor(Guid.NewGuid()));
            var aggregateSet = new AggregateSet(context, dateTime);

            var appDbContext = new AppDbContext(eventStore, aggregateSet);

            DbInitializer.Initialize(appDbContext, ConfigurationFactory.Create());

            appDbContext.SaveChangesAsync(default).GetAwaiter().GetResult();

            return appDbContext;
        }

        public AppDbContextBuilder()
        {
            _appDbContext = WithDefaults();
        }

        public AppDbContextBuilder Add<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : AggregateRoot
        {
            _appDbContext.Store(aggregateRoot);
            return this;
        }

        public AppDbContextBuilder SaveChanges()
        {
            _appDbContext.SaveChangesAsync(default).GetAwaiter().GetResult();

            return this;
        }

        public AppDbContext Build()
        {
            return _appDbContext;
        }
    }
}
