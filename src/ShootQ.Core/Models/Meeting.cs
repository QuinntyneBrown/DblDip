using BuildingBlocks.Abstractions;
using ShootQ.Core.Interfaces;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Meeting : AggregateRoot, IScheduled
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid MeetingId { get; private set; }

        public DateRange Scheduled { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
