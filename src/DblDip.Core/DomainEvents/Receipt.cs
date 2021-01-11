using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ReceiptCreated(Guid ReceiptId): Event;
    public record ReceiptRemoved(DateTime Deleted): Event;
    public record ReceiptUpdated: Event;
}
