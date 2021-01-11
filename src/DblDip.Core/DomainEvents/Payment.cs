using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PaymentCreated(Guid PaymentId): Event;
    public record PaymentUpdated: Event;
    public record PaymentRemoved(DateTime Deleted): Event;
}
