using System;

namespace DblDip.Core.Models
{
    public class TicketState
    {
        public TicketState(Guid ticketId, Guid stateId)
        {
            TicketId = ticketId;
            StateId = stateId;
        }

        public Guid TicketId { get; set; }
        public Guid StateId { get; set; }
    }
}
