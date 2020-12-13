using DblDip.Core.Models;
using DblDip.Core.ValueObjects;

namespace DblDip.Testing.Builders.Core.Models
{
    public class WeddingQuoteBuilder
    {
        private WeddingQuote _weddingQuote;

        public static WeddingQuote WithDefaults()
        {
            var rate = RateBuilder.WithDefaults();

            return new WeddingQuote((Email)"test@test.com", WeddingBuilder.WithDefaults(rate), rate);
        }

        public WeddingQuoteBuilder()
        {
            _weddingQuote = WithDefaults();
        }

        public WeddingQuote Build()
        {
            return _weddingQuote;
        }
    }
}
