using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record LeadCreated(Guid LeadId): Event;
    public record LeadRemoved(DateTime Deleted): Event;
}
