using BuildingBlocks.Abstractions;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Ticket: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Ticket()
        {

        }
        protected override void EnsureValidState()
        {

        }

        public Guid TicketId { get; private set; }
        public Guid EpicId { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string AcceptanceCriteria { get; private set; }
        public int Priority { get; private set; }
        public ICollection<Comment> Coments { get; private set; }
        public DateTime? Deleted { get; private set; }
    }

    public record TicketState(Guid TicketId, Guid StateId);    
}
