using System;

namespace DblDip.Domain.Features
{
    public class PointDto
    {
        public Guid PointId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
