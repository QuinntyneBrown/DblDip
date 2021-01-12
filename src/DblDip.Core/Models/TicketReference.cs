using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class TicketReference
    {
        public Guid TicketId { get; init; }
        protected TicketReference()
        {

        }
        public TicketReference(Guid ticketId)
        {
            TicketId = ticketId;
        }
    }
}
