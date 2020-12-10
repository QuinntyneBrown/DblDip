using ShootQ.Core.ValueObjects;

namespace ShootQ.Core.DomainEvents
{
    public record Rescheduled(DateRange newSchedule);
}
