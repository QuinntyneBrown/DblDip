using ShootQ.Core.Models;
using ShootQ.Domain.Features.Quotes;
using ShootQ.Domain.Features.Weddings;

namespace ShootQ.Domain.Features
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
