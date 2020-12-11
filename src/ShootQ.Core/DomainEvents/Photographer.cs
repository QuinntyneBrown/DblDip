using System;

namespace ShootQ.Core.DomainEvents
{
    public record PhotographerCreated (Guid PhotographerId);
    public record PhotographerCompanyAdded(Guid CompanyId);
}
