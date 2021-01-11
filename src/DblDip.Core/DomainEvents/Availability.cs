using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record AvailabilityCreated(Guid AvailabilityId): Event;
    public record AvailabilityRemoved(DateTime Deleted): Event;
    public record AvailabilityUpdated(): Event;
}
