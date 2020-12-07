using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record WeddingQuoteCreated(Guid WeddingQuoteId, Email Email, Guid WeddingId);

    public record QuoteItemAdded(Price Amount, string Description);
    
    public record QuoteAccepted(DateTime Accepted);

    public record QuoteCompleted(DateTime Completed);
    
    public record QuoteDeclined(DateTime Declined);
}
