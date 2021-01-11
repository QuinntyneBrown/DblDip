using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ShotAdded(string Value): Event;
    public record ShotRemoved(Guid ShotId): Event;
    public record ShotListCreated(Guid ShotListId): Event;
    public record ShotListUpdated: Event;
    public record ShotListRemoved(DateTime Deleted): Event;
}
