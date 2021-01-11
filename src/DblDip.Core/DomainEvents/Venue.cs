using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record VenueRemoved(DateTime Deleted): Event;
    public record VenueCreated(Guid VenueId): Event;
    public record VenueUpdated: Event;
}
