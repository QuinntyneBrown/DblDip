using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.WeddingQuotes
{
    public record WeddingQuoteDto(Guid WeddingQuoteId, Guid WeddingId, Price Total);
}
