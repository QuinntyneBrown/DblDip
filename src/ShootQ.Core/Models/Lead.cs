using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class Lead : AggregateRoot
    {
        public Lead()
        {
            Apply(new LeadCreated(Guid.NewGuid()));
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(LeadCreated leadCreated)
        {
            LeadId = leadCreated.LeadId;
        }

        protected void When(LeadRemoved leadRemoved)
        {
            Deleted = leadRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new LeadRemoved(deleted));
        }

        public Guid LeadId { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string EmailAddress { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
