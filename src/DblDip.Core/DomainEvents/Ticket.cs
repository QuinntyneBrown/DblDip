using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record TicketCreated (Guid TicketId): Event;
    public record TicketRemoved (DateTime Deleted): Event;
    public record TicketUpdated: Event;
}
