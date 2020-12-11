using System;

namespace DblDip.Domain.Features.Engagements
{
    public class EngagementDto
    {
        public Guid EngagementId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
