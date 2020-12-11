using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Admin : Profile
    {
        public Admin(string name, Email email)
            : base(new ProfileCreated(Guid.NewGuid(), name, email, nameof(Admin), typeof(Admin).AssemblyQualifiedName))
        {

        }
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid AdminId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
