using DblDip.Core.Models;
using DblDip.Domain.Features.Offers;

namespace DblDip.Testing.Builders
{
    public class OfferDtoBuilder
    {
        private OfferDto _offerDto;

        public static OfferDto WithDefaults()
        {
            return new OfferDto(default);
        }

        public OfferDtoBuilder()
        {
            _offerDto = WithDefaults();
        }

        public OfferDto Build()
        {
            return _offerDto;
        }
    }
}
