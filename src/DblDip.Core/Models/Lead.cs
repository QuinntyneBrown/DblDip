using DblDip.Core.DomainEvents;
using DblDip.Core.Enums;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Lead : Profile
    {
        public Guid LeadId { get; private set; }
        public LeadStatus Status { get; private set; }

        protected Lead()
        {

        }

        public Lead(Email email)
            : base(null, email, typeof(Lead))
        {
            Apply(new LeadCreated(base.ProfileId));
        }

        protected override void When(dynamic @event) => When(@event);

        protected void When(LeadCreated leadCreated)
        {
            LeadId = leadCreated.LeadId;
        }

        protected void When(LeadRemoved leadRemoved)
        {
            base.Deleted = leadRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public new void Remove(DateTime deleted)
        {
            base.Remove(deleted);
        }

    }
}
