using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Company : AggregateRoot
    {
        public Guid CompanyId { get; private set; }
        public Guid LogoDigitalAssetId { get; private set; }
        public Url Url { get; private set; }
        public DateTime? Deleted { get; private set; }
        protected override void When(dynamic @event) => When(@event);

        public Company()
        {
            Apply(new CompanyCreated(Guid.NewGuid()));
        }
        public void When(CompanyCreated companyCreated)
        {
            CompanyId = companyCreated.CompanyId;
        }

        public void When(CompanyRemoved companyRemoved)
        {
            Deleted = companyRemoved.Deleted;
        }

        public void When(CompanyUpdated companyUpdated)
        {

        }

        public void When(CompanyLogoChanged companyLogoChanged)
        {
            LogoDigitalAssetId = companyLogoChanged.LogoDigitalAssetId;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new CompanyRemoved(deleted));
        }

        public void Update()
        {
            Apply(new CompanyUpdated());
        }

        public void ChangeLogo(Guid logoDigitalAssetId)
        {
            Apply(new CompanyLogoChanged(logoDigitalAssetId));
        }
    }
}
