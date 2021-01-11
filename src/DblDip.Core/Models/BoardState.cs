using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    [Owned]
    public class BoardState
    {
        protected BoardState()
        {

        }

        public string Name { get; set; }
        public int SortOrder { get; set; } 
        public ICollection<TicketState> TicketStates { get; set; }
    }
}
