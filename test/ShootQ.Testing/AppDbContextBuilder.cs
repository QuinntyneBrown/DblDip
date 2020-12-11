using BuildingBlocks.EventStore;
using Microsoft.EntityFrameworkCore;
using ShootQ.Core.Models;
using System;

namespace ShootQ.Testing
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

            appDbContext.Store(new Role(ShootQ.Core.Constants.Roles.Client, nameof(ShootQ.Core.Constants.Roles.Client)));
            appDbContext.Store(new Role(ShootQ.Core.Constants.Roles.ProjectManager, nameof(ShootQ.Core.Constants.Roles.ProjectManager)));
            appDbContext.Store(new Role(ShootQ.Core.Constants.Roles.SystemAdministrator, nameof(ShootQ.Core.Constants.Roles.SystemAdministrator)));
            appDbContext.Store(new Role(ShootQ.Core.Constants.Roles.Photographer, nameof(ShootQ.Core.Constants.Roles.Photographer)));
            
            appDbContext.SaveChangesAsync(default).GetAwaiter().GetResult();

            return appDbContext;
        }

        public AppDbContextBuilder()
        {
            _appDbContext = WithDefaults();
        }

        public AppDbContext Build()
        {
            return _appDbContext;
        }
    }
}
