using ShootQ.Core.Models;
using ShootQ.Domain.Features.Offers;

namespace ShootQ.Domain.Features
{
    public static class OfferExtensions
    {
        public static OfferDto ToDto(this Offer offer)
            => new OfferDto(offer.OfferId);
    }
}
