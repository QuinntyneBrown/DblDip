using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Company: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Company(string value)
        {
            Apply(new CompanyCreated(value));
        }
        public void When(CompanyCreated companyCreated)
        {

        }

        public void When(CompanyRemoved companyRemoved)
        {

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

        public void Remove(string value)
        {
            Apply(new CompanyRemoved(value));
        }

        public void Update(string value)
        {
            Apply(new CompanyUpdated(value));
        }

        public void ChangeLogo(Guid logoDigitalAssetId)
        {
            Apply(new CompanyLogoChanged(logoDigitalAssetId));
        }

        public Guid CompanyId { get; private set; }
        public Guid LogoDigitalAssetId { get; private set; }
        public Url Url { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
