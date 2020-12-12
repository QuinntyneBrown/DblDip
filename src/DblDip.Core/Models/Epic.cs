using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Epic: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid EpicId { get; private set; }
        public Guid AuthorId { get; private set; }
        public ICollection<Guid> TicketIds { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
