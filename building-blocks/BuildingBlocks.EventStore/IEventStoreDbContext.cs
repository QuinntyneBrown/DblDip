using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventStore
{
    public interface IEventStoreDbContext
    {
        DbSet<StoredEvent> StoredEvents { get; }
        DbSet<SnapShot> SnapShots { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry<TEntity> Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}
