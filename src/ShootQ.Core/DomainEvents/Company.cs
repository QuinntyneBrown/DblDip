using System;

namespace ShootQ.Core.DomainEvents
{
    public record CompanyCreated (string Value);
    public record CompanyLogoChanged(Guid LogoDigitalAssetId);
    public record CompanyRemoved(string Value);
    public record CompanyUpdated(string Value);
}
