using DblDip.Domain.Features.WeddingQuotes;

namespace DblDip.Testing.Builders
{
    public class WeddingQuoteDtoBuilder
    {
        private WeddingQuoteDto _weddingQuoteDto;

        public static WeddingQuoteDto WithDefaults()
        {
            return new WeddingQuoteDto(default, default, default);
        }

        public WeddingQuoteDtoBuilder()
        {
            _weddingQuoteDto = WithDefaults();
        }

        public WeddingQuoteDto Build()
        {
            return _weddingQuoteDto;
        }
    }
}
