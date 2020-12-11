using System;

namespace DblDip.Domain.Features.Points
{
    public class PointDto
    {
        public Guid PointId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
