using System;

namespace DblDip.Domain.Features.Participants
{
    public class ParticipantDto
    {
        public Guid ParticipantId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
