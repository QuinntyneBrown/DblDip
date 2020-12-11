using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Photographer : Profile
    {
        protected override void When(dynamic @event) => When(@event);

        public Photographer(string name, Email email)
            : base(new ProfileCreated(Guid.NewGuid(), name, email, nameof(Client), typeof(Client).AssemblyQualifiedName))
        {
            Apply(new PhotographerCreated(base.ProfileId));
        }

        public void When(PhotographerCreated photographerCreated)
        {
            PhotographerId = photographerCreated.PhotographerId;
        }

        public void When(PhotographerCompanyAdded photographerCompanyAdded)
        {
            CompanyId = photographerCompanyAdded.CompanyId;
        }

        protected override void EnsureValidState()
        {

        }

        public void AddCompany(Guid companyId)
        {
            Apply(new PhotographerCompanyAdded(companyId));
        }

        public Guid PhotographerId { get; private set; }
        public Guid CompanyId { get; private set; }
        public ICollection<Guid> ServiceIds { get; private set; }
        public Location PrimaryLocation { get; set; }
    }
}
