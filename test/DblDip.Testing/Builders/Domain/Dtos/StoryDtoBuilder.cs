using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class StoryDtoBuilder
    {
        private StoryDto _storyDto;

        public static StoryDto WithDefaults()
        {
            return new StoryDto();
        }

        public StoryDtoBuilder()
        {
            _storyDto = WithDefaults();
        }

        public StoryDto Build()
        {
            return _storyDto;
        }
    }
}
