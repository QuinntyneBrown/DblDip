using BuildingBlocks.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBlocks.EventStore
{
    public interface IAggregateSet
    {
        IQueryable<TAggregateRoot> Set<TAggregateRoot>()
            where TAggregateRoot : AggregateRoot;

        Task<TAggregateRoot> FindAsync<TAggregateRoot>(Guid id)
            where TAggregateRoot : AggregateRoot;
    }
}
