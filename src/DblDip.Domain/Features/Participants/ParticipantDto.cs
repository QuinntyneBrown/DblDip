using System;

namespace DblDip.Domain.Features.Participants
{
    public class ParticipantDto
    {
        public Guid ParticipantId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
