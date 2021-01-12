using BuildingBlocks.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BuildingBlocks.EventStore
{
    public interface IEventStore: IDbContext
    {
        DbSet<StoredEvent> StoredEvents { get; }
        Task<TAggregateRoot> LoadAsync<TAggregateRoot>(Guid id)
            where TAggregateRoot : AggregateRoot;
        void Add(IAggregateRoot aggregateRoot);

    }
}
