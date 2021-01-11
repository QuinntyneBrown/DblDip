using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ParticipantUpdated: Event;
    public record ParticipantCreated(Guid ParticipantId): Event;
    public record ParticipantRemoved(DateTime Deleted): Event;
}
