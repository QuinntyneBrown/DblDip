using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Timeline: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid TimelineId { get; private set; }
    }
}
