using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventStore
{
    public interface IEventStore
    {
        DbSet<StoredEvent> StoredEvents { get; }
        void Add(IAggregateRoot aggregateRoot);
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        //ValueTask<object> FindAsync(Type entityType, params object[] keyValues);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
