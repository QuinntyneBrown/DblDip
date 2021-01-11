using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record BoardCreated(Guid BoardId, string Name): Event;
    public record BoardUpdated(string Value): Event;
    public record BoardRemoved(DateTime Deleted): Event;
}
