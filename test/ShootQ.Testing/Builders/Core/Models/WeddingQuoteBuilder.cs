using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class WeddingQuoteBuilder
    {
        private WeddingQuote _weddingQuote;

        public static WeddingQuote WithDefaults()
        {
            return new WeddingQuote(default, default, default);
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
