using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features
{
    public record WeddingQuoteDto(Guid WeddingQuoteId, Guid WeddingId, Price Total);
}
