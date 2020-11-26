using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class Lead: AggregateRoot
    {
        public Lead()
        {
            Apply(new LeadCreated());
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(LeadCreated leadCreated)
        {
            LeadId = leadCreated.LeadId;
        }

        public void When(LeadRemoved leadRemoved)
        {
            Deleted = leadRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(string value)
        {
            Apply(new LeadRemoved());
        }

        public Guid LeadId { get; private set; }
        public DateTime? Deleted { get; set; }
    }
}
