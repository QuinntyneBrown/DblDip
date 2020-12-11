using BuildingBlocks.EventStore;
using Microsoft.EntityFrameworkCore;
using DblDip.Core.Models;
using System;

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

            appDbContext.Store(new Role(DblDip.Core.Constants.Roles.Client, nameof(DblDip.Core.Constants.Roles.Client)));
            appDbContext.Store(new Role(DblDip.Core.Constants.Roles.ProjectManager, nameof(DblDip.Core.Constants.Roles.ProjectManager)));
            appDbContext.Store(new Role(DblDip.Core.Constants.Roles.SystemAdministrator, nameof(DblDip.Core.Constants.Roles.SystemAdministrator)));
            appDbContext.Store(new Role(DblDip.Core.Constants.Roles.Photographer, nameof(DblDip.Core.Constants.Roles.Photographer)));
            
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
