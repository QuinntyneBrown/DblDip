using DblDip.Domain.Features.SocialEvents;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class SocialEventDtoBuilder
    {
        private SocialEventDto _socialEventDto;

        public static SocialEventDto WithDefaults()
        {
            return new SocialEventDto();
        }

        public SocialEventDtoBuilder()
        {
            _socialEventDto = new SocialEventDto();
        }

        public SocialEventDto Build()
        {
            return _socialEventDto;
        }
    }
}
