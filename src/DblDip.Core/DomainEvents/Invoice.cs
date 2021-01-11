using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record InvoiceCreated(Guid InvoiceId): Event;
    public record InvoiceRemoved (DateTime Deleted): Event;
    public record InvoiceUpdated: Event;
}
