using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Referral : AggregateRoot
    {
        public Guid ReferralId { get; private set; }
        public DateTime? Deleted { get; private set; }
        public Referral()
        {
            Apply(new ReferralCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(ReferralCreated referralCreated)
        {
            ReferralId = referralCreated.ReferralId;
        }

        public void When(ReferralUpdated referralUpdated)
        {

        }

        public void When(ReferralRemoved referralRemoved)
        {
            Deleted = referralRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new ReferralRemoved(deleted));
        }
    }
}
