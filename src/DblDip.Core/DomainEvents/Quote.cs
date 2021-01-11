using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record WeddingQuoteCreated(Guid WeddingQuoteId, Email BillToEmail, Guid WeddingId): Event;

    public record QuoteItemAdded(Price Amount, string Description): Event;

    public record QuoteAccepted(DateTime Accepted): Event;

    public record QuoteCompleted(DateTime Completed): Event;

    public record QuoteDeclined(DateTime Declined): Event;

    public record QuoteCreated(Guid QuoteId): Event;
}
