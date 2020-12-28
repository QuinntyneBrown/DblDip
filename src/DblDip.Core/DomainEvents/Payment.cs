using System;

namespace DblDip.Core.DomainEvents
{
    public record PaymentCreated (Guid PaymentId);
    public record PaymentUpdated;
    public record PaymentRemoved (DateTime Deleted);
}
