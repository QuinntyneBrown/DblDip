using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
