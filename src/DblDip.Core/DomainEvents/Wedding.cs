using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record WeddingCreated(Location Start, Location End, Location Location, Guid WeddingId, DateTime DateTime, int Hours): Event;
    public record WeddingPartAdded(DateTime DateTime, int Hours, Location Location, string Description): Event;
}
