using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Epic : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid EpicId { get; private set; }
        public Guid AuthorId { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyList<TicketReference> TicketReferences => _ticketReferences.ToList();
        public DateTime? Deleted { get; private set; }
        private IEnumerable<TicketReference> _ticketReferences;
    }
}
