using System;

namespace DblDip.Core.DomainEvents
{
    public record InvoiceCreated(Guid InvoiceId);
    public record InvoiceRemoved (DateTime Deleted);
    public record InvoiceUpdated;
}
