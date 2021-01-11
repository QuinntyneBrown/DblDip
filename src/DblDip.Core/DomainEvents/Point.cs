using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PointCreated (Guid PointId): Event;
    public record PointUpdated: Event;
    public record PointRemoved (DateTime Deleted): Event;
}
