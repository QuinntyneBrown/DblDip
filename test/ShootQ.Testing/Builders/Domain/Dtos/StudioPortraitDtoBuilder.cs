using ShootQ.Domain.Features.StudioPortraits;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class StudioPortraitDtoBuilder
    {
        private StudioPortraitDto _studioPortraitDto;

        public static StudioPortraitDto WithDefaults()
        {
            return new StudioPortraitDto(default);
        }

        public StudioPortraitDtoBuilder()
        {
            _studioPortraitDto = WithDefaults();
        }

        public StudioPortraitDto Build()
        {
            return _studioPortraitDto;
        }
    }
}
