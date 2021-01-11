using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotographerCreated(Guid PhotographerId): Event;
    public record PhotographerCompanyAdded(Guid CompanyId): Event;
    public record PhotographerUpdated: Event;
}
