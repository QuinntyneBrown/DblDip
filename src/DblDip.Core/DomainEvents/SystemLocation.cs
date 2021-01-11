using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record SystemLocationCreated(Guid SystemLocationId, string Name, Location Location): Event;
    public record SystemLocationRemoved(DateTime Deleted): Event;
}
