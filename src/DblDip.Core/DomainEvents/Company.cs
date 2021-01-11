using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record CompanyCreated(Guid CompanyId): Event;
    public record CompanyLogoChanged(Guid LogoDigitalAssetId): Event;
    public record CompanyRemoved(DateTime Deleted): Event;
    public record CompanyUpdated: Event;
}
