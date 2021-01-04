using System;

namespace DblDip.Domain.Features
{
    public class AvailabilityDto
    {
        public Guid AvailabilityId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
