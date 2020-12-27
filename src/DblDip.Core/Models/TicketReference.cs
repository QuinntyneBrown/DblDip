using System;

namespace DblDip.Core.Models
{
    public class TicketReference
    {
        public TicketReference(Guid ticketId, string title)
        {
            TicketId = ticketId;
            Title = title;
        }
        public Guid TicketId { get; init; }
        public string Title { get; init; }
    }
}
