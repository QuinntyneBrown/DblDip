using BuildingBlocks.EventStore;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.DomainEvents
{
    public record RateCreated(Guid PhotographyRateId, Price Price, string Name): Event;
    public record RateUpdated (string Value): Event;
    public record RateRemoved (DateTime Deleted): Event;
}
