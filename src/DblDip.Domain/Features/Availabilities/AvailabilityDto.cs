using System;

namespace DblDip.Domain.Features.Availabilities
{
    public class AvailabilityDto
    {
        public Guid AvailabilityId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
