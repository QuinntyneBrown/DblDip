using DblDip.Core.Models;
using DblDip.Domain.Features.Quotes;
using DblDip.Domain.Features.Weddings;

namespace DblDip.Domain.Features
{
    public static class QuoteExtensions
    {
        public static QuoteDto ToDto(this Quote quote)
        {
            return new QuoteDto
            {
                Total = quote.Total,
                LineItems = quote.LineItems
            };
        }
    }
}
