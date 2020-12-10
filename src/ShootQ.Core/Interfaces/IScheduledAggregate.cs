using BuildingBlocks.Core;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Core.Interfaces
{
    public interface IScheduled
    {
        DateRange Scheduled { get; }

        public bool Overlap(IScheduled scheduled)
        {
            Guard.ArgumentNotNull(nameof(scheduled), scheduled);

            return Scheduled.Overlap(scheduled.Scheduled);
        }
    }
    public interface IScheduledAggregate : IScheduled
    {
        void Apply(object @event);

        public void Reschedule(DateRange newSchedule)
        {
            Apply(new Rescheduled(newSchedule));
        }
    }
}
