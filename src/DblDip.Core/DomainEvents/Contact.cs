using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ContactCreated(Guid ContactId): Event;
    public record ContactRemoved(DateTime Deleted): Event;
    public record ContactUpdated(): Event;
}
