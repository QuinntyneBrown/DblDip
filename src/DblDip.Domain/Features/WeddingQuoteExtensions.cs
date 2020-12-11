using DblDip.Core.Models;
using DblDip.Domain.Features.WeddingQuotes;

namespace DblDip.Domain.Features
{
    public static class WeddingQuoteExtensions
    {
        public static WeddingQuoteDto ToDto(this WeddingQuote weddingQuote)
            => new WeddingQuoteDto(weddingQuote.WeddingQuoteId, weddingQuote.WeddingId, weddingQuote.Total);
    }
}
