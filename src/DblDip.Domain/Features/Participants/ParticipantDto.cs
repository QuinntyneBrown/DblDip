using System;

namespace DblDip.Domain.Features
{
    public class ParticipantDto
    {
        public Guid ParticipantId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
