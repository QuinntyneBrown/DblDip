﻿using BuildingBlocks.Core;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Core.Interfaces
{
    public interface IScheduled
    {
        DateRange Scheduled { get; }
        void Apply(object @event);
        public bool Overlap(IScheduled scheduled)
        {
            Guard.ArgumentNotNull(nameof(scheduled), scheduled);

            return Scheduled.Overlap(scheduled.Scheduled);
        }

        public void Reschedule(DateRange newSchedule)
        {
            Apply(new Rescheduled(newSchedule));
        }
    }
}
