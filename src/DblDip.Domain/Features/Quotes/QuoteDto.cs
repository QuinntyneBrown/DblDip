using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using static DblDip.Core.Models.Quote;

namespace DblDip.Domain.Features.Quotes
{
    public class QuoteDto
    {
        public Guid QuoteId { get; set; }
        public Email Email { get; set; }
        public Price Total { get; set; }
        public ICollection<LineItem> LineItems { get; set; }

    }
}
