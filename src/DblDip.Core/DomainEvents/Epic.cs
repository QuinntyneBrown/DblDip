using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record EpicCreated (Guid EpicId): Event;
    public record EpicUpdated: Event;
    public record EpicRemoved (DateTime Deleted): Event;
}
