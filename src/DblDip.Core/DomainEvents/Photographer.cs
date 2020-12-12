using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotographerCreated(Guid PhotographerId);
    public record PhotographerCompanyAdded(Guid CompanyId);
}
