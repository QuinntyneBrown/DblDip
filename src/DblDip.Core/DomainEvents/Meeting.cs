using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record MeetingCreated (Guid MeetingId): Event;
    public record MeetingUpdated: Event;
    public record MeetingRemoved (DateTime Deleted): Event;
}
