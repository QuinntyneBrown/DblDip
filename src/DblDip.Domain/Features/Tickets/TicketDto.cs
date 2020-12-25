using System;

namespace DblDip.Domain.Features.Tickets
{
    public class TicketDto
    {
        public Guid TicketId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
