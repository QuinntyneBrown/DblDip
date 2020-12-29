using System;

namespace DblDip.Core.DomainEvents
{
    public record PaymentScheduleCreated (Guid PaymentScheduleId);
    public record PaymentScheduleUpdated;
    public record PaymentScheduleRemoved (DateTime Deleted);
}
