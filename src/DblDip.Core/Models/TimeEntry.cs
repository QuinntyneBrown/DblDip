using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class TimeEntry : AggregateRoot
    {
        public TimeEntry()
        {
            Apply(new TimeEntryCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(TimeEntryRemoved timeEntryRemoved)
        {
            Deleted = timeEntryRemoved.Deleted;
        }

        public void When(TimeEntryCreated timeEntryCreated)
        {
            TimeEntryId = timeEntryCreated.TimeEntryId;
        }

        public void When(TimeEntryUpdated timeEntryUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new TimeEntryRemoved(deleted));
        }

        public void Update()
        {
            Apply(new TimeEntryUpdated());
        }

        public Guid TimeEntryId { get; private set; }
        public int Hours { get; private set; }
        public DateTime? Completed { get; private set; }
        public Guid ProjectId { get; private set; }
        public bool Billable { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
