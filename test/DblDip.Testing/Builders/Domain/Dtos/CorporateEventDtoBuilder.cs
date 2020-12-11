using DblDip.Domain.Features.CorporateEvents;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class CorporateEventDtoBuilder
    {
        private CorporateEventDto _corporateEventDto;

        public static CorporateEventDto WithDefaults()
        {
            return new CorporateEventDto(default);
        }

        public CorporateEventDtoBuilder()
        {
            _corporateEventDto = WithDefaults();
        }

        public CorporateEventDto Build()
        {
            return _corporateEventDto;
        }
    }
}
