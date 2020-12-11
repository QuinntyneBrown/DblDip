using System;

namespace ShootQ.Domain.Features.Availabilities
{
    public class AvailabilityDto
    {
        public Guid AvailabilityId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
