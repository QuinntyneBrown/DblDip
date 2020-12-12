using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class TimeEntry : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid TimeEntryId { get; private set; }
        public int Hours { get; private set; }
        public DateTime? Completed { get; private set; }
        public Guid ProjectId { get; private set; }
        public bool Billable { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
