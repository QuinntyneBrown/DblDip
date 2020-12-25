using System;

namespace DblDip.Domain.Features.Points
{
    public class PointDto
    {
        public Guid PointId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
