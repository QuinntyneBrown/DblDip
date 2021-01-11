using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record BlogCreated(Guid BlogId): Event;
    public record BlogPostAdded(Guid PostId, string Title): Event;
    public record BlogRemoved(DateTime Deleted): Event;
    public record BlogUpdated: Event;
}
