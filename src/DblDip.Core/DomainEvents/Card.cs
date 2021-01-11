using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record CardCreated(Guid CardId, string Name, string Description): Event;
    public record CardRemoved(DateTime Deleted): Event;
    public record CardUpdated: Event;
}
