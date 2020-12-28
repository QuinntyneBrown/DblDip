using System;

namespace DblDip.Core.DomainEvents
{
    public record VenueRemoved(DateTime Deleted);
    public record VenueCreated(Guid VenueId);
    public record VenueUpdated;
}
