using BuildingBlocks.Abstractions;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Meeting : AggregateRoot, IScheduledAggregate
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid MeetingId { get; private set; }

        public DateRange Scheduled { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deleted { get; private set; }
    }
}
