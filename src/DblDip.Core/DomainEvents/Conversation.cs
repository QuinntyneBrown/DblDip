using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ConversationUpdated: Event;
    public record ConversationCreated(Guid ConversationId): Event;
    public record ConversationRemoved(DateTime Deleted): Event;
}
