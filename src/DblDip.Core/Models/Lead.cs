using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.Enums;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Lead : Profile
    {
        public Lead(Email email)
            : base(new ProfileCreated(Guid.NewGuid(), null, email, nameof(Lead), typeof(Lead).AssemblyQualifiedName))
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
        public Email Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime? Deleted { get; private set; }
        public LeadStatus Status { get; private set; }
    }
}
