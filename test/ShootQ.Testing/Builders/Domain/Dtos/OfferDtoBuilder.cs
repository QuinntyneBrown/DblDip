using ShootQ.Core.Models;
using ShootQ.Domain.Features.Offers;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
