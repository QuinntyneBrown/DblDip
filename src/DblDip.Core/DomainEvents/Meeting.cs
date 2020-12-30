using System;

namespace DblDip.Core.DomainEvents
{
    public record MeetingCreated (Guid MeetingId);
    public record MeetingUpdated;
    public record MeetingRemoved (DateTime Deleted);
}
