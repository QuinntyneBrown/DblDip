using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.WeddingQuotes
{
    public record WeddingQuoteDto(Guid WeddingQuoteId, Guid WeddingId, Price Total);
}
