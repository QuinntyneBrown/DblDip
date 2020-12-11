using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Referral : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid ReferralId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
