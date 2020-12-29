using System;

namespace DblDip.Core.DomainEvents
{
    public record TicketCreated (Guid TicketId);
    public record TicketRemoved (DateTime Deleted);
    public record TicketUpdated;
}
