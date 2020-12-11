using DblDip.Domain.Features.SystemLocations;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class SystemLocationDtoBuilder
    {
        private SystemLocationDto _systemLocationDto;

        public static SystemLocationDto WithDefaults()
        {
            return new SystemLocationDto();
        }

        public SystemLocationDtoBuilder()
        {
            _systemLocationDto = new SystemLocationDto();
        }

        public SystemLocationDto Build()
        {
            return _systemLocationDto;
        }
    }
}
