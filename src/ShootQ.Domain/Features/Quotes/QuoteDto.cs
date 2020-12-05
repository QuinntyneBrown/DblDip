using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;
using static ShootQ.Core.Models.Quote;

namespace ShootQ.Domain.Features.Quotes
{
    public class QuoteDto
    {
        public Guid QuoteId { get; set; }
        public Email Email { get; set; }
        public Price Total { get; set; }
        public ICollection<LineItem> LineItems { get; set; }

    }
}
