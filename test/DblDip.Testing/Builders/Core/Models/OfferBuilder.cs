using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class OfferBuilder
    {
        private Offer _offer;

        public static Offer WithDefaults()
        {
            return new Offer();
        }

        public OfferBuilder()
        {
            _offer = WithDefaults();
        }

        public Offer Build()
        {
            return _offer;
        }
    }
}
