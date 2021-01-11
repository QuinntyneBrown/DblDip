using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record BrandCreated(Guid BrandId): Event;
    public record BrandRemoved(DateTime Deleted): Event;
    public record BrandUpdated(): Event;
}
