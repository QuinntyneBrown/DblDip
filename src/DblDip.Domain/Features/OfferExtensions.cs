using DblDip.Core.Models;
using DblDip.Domain.Features.Offers;

namespace DblDip.Domain.Features
{
    public static class OfferExtensions
    {
        public static OfferDto ToDto(this Offer offer)
            => new OfferDto(offer.OfferId);
    }
}
