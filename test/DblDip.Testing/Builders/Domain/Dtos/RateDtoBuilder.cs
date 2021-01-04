using DblDip.Domain.Features.Rates;

namespace DblDip.Testing.Builders
{
    public class RateDtoBuilder
    {
        private RateDto _rateDto;

        public static RateDto WithDefaults()
        {
            return new RateDto();
        }

        public RateDtoBuilder()
        {
            _rateDto = new RateDto();
        }

        public RateDto Build()
        {
            return _rateDto;
        }
    }
}
