using System;

namespace DblDip.Domain.Features.Messages
{
    public class MessageDto
    {
        public Guid MessageId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
