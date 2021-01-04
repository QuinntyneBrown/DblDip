using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using static DblDip.Core.Models.Quote;

namespace DblDip.Domain.Features
{
    public class QuoteDto
    {
        public Guid QuoteId { get; init; }
        public Email Email { get; init; }
        public Price Total { get; init; }
        public ICollection<LineItem> LineItems { get; init; }

    }
}
