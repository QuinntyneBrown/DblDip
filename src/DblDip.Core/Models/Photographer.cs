using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DblDip.Core.Models
{
    public class Photographer : Profile
    {
        protected Photographer()
        {

        }

        protected override void When(dynamic @event) => When(@event);

        public Photographer(string name, Email email)
            : base(name, email, typeof(Photographer))
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

        public void When(PhotographerUpdated photographerUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void AddCompany(Guid companyId)
        {
            Apply(new PhotographerCompanyAdded(companyId));
        }

        public void Update()
        {
            Apply(new PhotographerUpdated());
        }

        public Guid PhotographerId { get; private set; }
        public Guid CompanyId { get; private set; }
        [NotMapped]
        public ICollection<Guid> ServiceIds { get; private set; }
        public Location PrimaryLocation { get; private set; }
    }
}
