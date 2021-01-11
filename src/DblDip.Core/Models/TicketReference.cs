using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class TicketReference
    {
        protected TicketReference()
        {

        }

        public Guid TicketId { get; init; }
        public string Title { get; init; }
        public TicketReference(Guid ticketId, string title)
        {
            TicketId = ticketId;
            Title = title;
        }
    }
}
