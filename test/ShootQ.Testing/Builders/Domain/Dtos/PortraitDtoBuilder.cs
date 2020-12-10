using ShootQ.Domain.Features.Portraits;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class PortraitDtoBuilder
    {
        private PortraitDto _portraitDto;

        public static PortraitDto WithDefaults()
        {
            return new PortraitDto(default);
        }

        public PortraitDtoBuilder()
        {
            _portraitDto = WithDefaults();
        }

        public PortraitDto Build()
        {
            return _portraitDto;
        }
    }
}