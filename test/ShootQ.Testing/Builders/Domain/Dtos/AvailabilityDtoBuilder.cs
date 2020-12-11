using ShootQ.Core.Models;
using ShootQ.Domain.Features.Availabilities;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class AvailabilityDtoBuilder
    {
        private AvailabilityDto _availabilityDto;

        public static AvailabilityDto WithDefaults()
        {
            return new AvailabilityDto();
        }

        public AvailabilityDtoBuilder()
        {
            _availabilityDto = WithDefaults();
        }

        public AvailabilityDto Build()
        {
            return _availabilityDto;
        }
    }
}
