using System;

namespace DblDip.Core.DomainEvents
{
    public record ConversationUpdated;
    public record ConversationCreated(Guid ConversationId);
    public record ConversationRemoved(DateTime Deleted);
}
