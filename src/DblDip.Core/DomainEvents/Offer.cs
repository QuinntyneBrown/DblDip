using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record OfferCreated(Guid OfferId): Event;
    public record OfferRemoved(DateTime Deleted): Event;
    public record OfferUpdated: Event;
}
