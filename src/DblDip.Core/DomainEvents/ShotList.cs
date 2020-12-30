using System;

namespace DblDip.Core.DomainEvents
{
    public record ShotAdded(string Value);
    public record ShotRemoved(Guid ShotId);
    public record ShotListCreated(Guid ShotListId);
    public record ShotListUpdated;
    public record ShotListRemoved(DateTime Deleted);
}
