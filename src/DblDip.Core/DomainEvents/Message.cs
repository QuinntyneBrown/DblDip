using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record MessageRemoved(DateTime Deleted): Event;
    public record MessageCreated(Guid MessageId): Event;
    public record MessageUpdated: Event;
}
