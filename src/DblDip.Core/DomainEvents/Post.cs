using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PostRemoved(DateTime Deleted): Event;
    public record PostPublished(DateTime Published): Event;
    public record PostCreated(Guid PostId, Guid AuthorId, string Title): Event;
    public record PostBodyUpdated(string Body): Event;
    public record PostTitleUpdated(string Title): Event;
}
