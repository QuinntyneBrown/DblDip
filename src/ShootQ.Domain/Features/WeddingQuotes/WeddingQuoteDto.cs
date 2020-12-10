using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.WeddingQuotes
{
    public class WeddingQuoteDto
    {
        public Guid WeddingQuoteId { get; set; }
        public Guid WeddingId { get; set; }
        public Price Total { get; set; }
    }
}
