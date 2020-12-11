using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class SystemAdministrator: Profile
    {
        protected override void When(dynamic @event) => When(@event);

        public SystemAdministrator(string name, Email email)
            : base(new ProfileCreated(Guid.NewGuid(), name, email, nameof(Client), typeof(Client).AssemblyQualifiedName))
        {
            Apply(new SystemAdministratorCreated(default));
        }

        public void When(SystemAdministratorCreated systemAdministratorCreated)
        {

        }

        public void When(SystemAdministratorUpdated systemAdministratorUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Update(string value)
        {
            Apply(new SystemAdministratorUpdated(value));
        }

        public Guid SystemAdministratorId { get; private set; }
    }
}
