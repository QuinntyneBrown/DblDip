using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class TicketState
    {
        protected TicketState()
        {

        }

        public Guid TicketId { get; init; }
        public Guid StateId { get; init; }
        public TicketState(Guid ticketId, Guid stateId)
        {
            TicketId = ticketId;
            StateId = stateId;
        }
    }
}
