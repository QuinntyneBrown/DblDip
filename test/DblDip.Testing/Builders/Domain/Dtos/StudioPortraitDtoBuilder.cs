using DblDip.Domain.Features.StudioPortraits;

namespace DblDip.Testing.Builders
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
