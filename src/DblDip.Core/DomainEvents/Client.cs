using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ClientCreated(Guid ClientId): Event;
    public record ClientUpdated: Event;
}
