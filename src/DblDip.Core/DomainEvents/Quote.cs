using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record WeddingQuoteCreated(Guid WeddingQuoteId, Email BillToEmail, Guid WeddingId);

    public record QuoteItemAdded(Price Amount, string Description);

    public record QuoteAccepted(DateTime Accepted);

    public record QuoteCompleted(DateTime Completed);

    public record QuoteDeclined(DateTime Declined);

    public record QuoteCreated(Guid QuoteId);
}
