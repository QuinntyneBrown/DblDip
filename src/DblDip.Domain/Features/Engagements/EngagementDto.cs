using System;

namespace DblDip.Domain.Features
{
    public class EngagementDto
    {
        public Guid EngagementId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
