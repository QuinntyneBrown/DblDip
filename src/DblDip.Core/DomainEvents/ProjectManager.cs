using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ProjectManagerCreated(Guid ProjectManagerId): Event;
    public record ProjectManagerUpdated: Event;
}
