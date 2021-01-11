using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PaymentScheduleCreated (Guid PaymentScheduleId): Event;
    public record PaymentScheduleUpdated: Event;
    public record PaymentScheduleRemoved (DateTime Deleted): Event;
}
