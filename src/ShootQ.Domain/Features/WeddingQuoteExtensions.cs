using ShootQ.Core.Models;
using ShootQ.Domain.Features.WeddingQuotes;

namespace ShootQ.Domain.Features
{
    public static class WeddingQuoteExtensions
    {
        public static WeddingQuoteDto ToDto(this WeddingQuote weddingQuote)
            => new WeddingQuoteDto(weddingQuote.WeddingQuoteId, weddingQuote.WeddingId, weddingQuote.Total);
    }
}
