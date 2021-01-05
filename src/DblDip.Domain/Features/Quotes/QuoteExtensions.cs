using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features;

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
