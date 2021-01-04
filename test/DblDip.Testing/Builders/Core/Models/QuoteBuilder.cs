using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Testing.Builders
{
    public class QuoteBuilder
    {
        private Quote _quote;

        public static Quote WithDefaults()
        {
            var rate = RateBuilder.WithDefaults();

            var wedding = WeddingBuilder.WithDefaults(rate);

            return new WeddingQuote((Email)"test@test.com", wedding, rate);
        }
        public QuoteBuilder()
        {

        }

        public Quote Build()
        {
            return _quote;
        }
    }
}
