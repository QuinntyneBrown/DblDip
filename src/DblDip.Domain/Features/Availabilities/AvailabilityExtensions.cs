using DblDip.Core.Models;

namespace DblDip.Domain.Features
{
    public static class AvailabilityExtensions
    {
        public static AvailabilityDto ToDto(this Availability availability)
        {
            return new AvailabilityDto
            {
                AvailabilityId = availability.AvailabilityId

            };
        }
    }
}
