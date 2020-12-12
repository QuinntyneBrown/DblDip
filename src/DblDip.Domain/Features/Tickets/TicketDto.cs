using System;

namespace DblDip.Domain.Features.Tickets
{
    public class TicketDto
    {
        public Guid TicketId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
