using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record TaskCreated (Guid TaskId, Guid OwnerId, string Description): Event;
    public record TaskUpdated: Event;
    public record TaskRemoved (DateTime Deleted): Event;
    public record TaskCompleted (DateTime Completed): Event;
}
