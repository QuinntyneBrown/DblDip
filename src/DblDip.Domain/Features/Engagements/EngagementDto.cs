using System;

namespace DblDip.Domain.Features.Engagements
{
    public class EngagementDto
    {
        public Guid EngagementId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
