using System;

namespace DblDip.Domain.Features.Conversations
{
    public class ConversationDto
    {
        public Guid ConversationId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
