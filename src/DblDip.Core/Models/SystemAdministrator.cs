using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class SystemAdministrator : Profile
    {
        public Guid SystemAdministratorId { get; private set; }
        protected SystemAdministrator()
        {

        }

        protected override void When(dynamic @event) => When(@event);

        public SystemAdministrator(string name, Email email)
            : base(name, email, typeof(SystemAdministrator))
        {
            Apply(new SystemAdministratorCreated(base.ProfileId));
        }

        public void When(SystemAdministratorCreated systemAdministratorCreated)
        {
            SystemAdministratorId = systemAdministratorCreated.SystemAdministratorId;
        }

        public void When(SystemAdministratorUpdated systemAdministratorUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {
            Apply(new SystemAdministratorUpdated());
        }
    }
}
