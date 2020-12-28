using System;

namespace DblDip.Core.DomainEvents
{
    public record CompanyCreated(Guid CompanyId);
    public record CompanyLogoChanged(Guid LogoDigitalAssetId);
    public record CompanyRemoved(DateTime Deleted);
    public record CompanyUpdated;
}
