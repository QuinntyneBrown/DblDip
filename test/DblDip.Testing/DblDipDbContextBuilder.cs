using BuildingBlocks.EventStore;
using DblDip.Core.Data;
using DblDip.Data;
using DblDip.Testing.Factories;
using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Testing
{
    public class DblDipDbContextBuilder
    {
        private DblDipDbContext _context;

        public static DblDipDbContext WithDefaults()
        {
            var options = new DbContextOptionsBuilder<DblDipDbContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options;


            var appDbContext = new DblDipDbContext(options);

            return appDbContext;
        }

        public DblDipDbContextBuilder()
        {
            _context = WithDefaults();
        }

        public DblDipDbContextBuilder Add<TAggregateRoot>(TAggregateRoot aggregateRoot)
            where TAggregateRoot : AggregateRoot
        {
            _context.Add(aggregateRoot);
            return this;
        }

        public DblDipDbContextBuilder SaveChanges()
        {
            _context.SaveChangesAsync(default).GetAwaiter().GetResult();

            return this;
        }

        public DblDipDbContext Build()
        {
            return _context;
        }
    }
}
