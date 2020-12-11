using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Engagement: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid EngagementId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
