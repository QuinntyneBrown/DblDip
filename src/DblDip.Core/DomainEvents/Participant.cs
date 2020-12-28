using System;

namespace DblDip.Core.DomainEvents
{
    public record ParticipantUpdated;
    public record ParticipantCreated (Guid ParticipantId);
    public record ParticipantRemoved (DateTime Deleted);
}
