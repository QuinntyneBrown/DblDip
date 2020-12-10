using BuildingBlocks.Abstractions;
using ShootQ.Core.Interfaces;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Task : AggregateRoot, IScheduledAggregate
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid TaskId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateRange Scheduled { get; private set; }
    }
}
