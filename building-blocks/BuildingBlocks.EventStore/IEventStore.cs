using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventStore
{
    public interface IEventStore
    {
        void Store(AggregateRoot aggregateRoot);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<EventStream> LoadEventStreamAsync(Guid eventStreamId);

        Task AppendToStreamAsync(Guid aggregateId, int streamVersion, List<object> events);
    }
}
