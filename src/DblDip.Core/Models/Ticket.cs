using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Ticket : AggregateRoot
    {
        public Guid TicketId { get; private set; }
        public Guid EpicId { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string AcceptanceCriteria { get; private set; }
        public int Priority { get; private set; }
        public ICollection<Comment> Coments { get; private set; }
        public DateTime? Deleted { get; private set; }
        public Ticket()
        {
            Apply(new TicketCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(TicketCreated ticketCreated)
        {
            TicketId = ticketCreated.TicketId;
        }

        public void When(TicketRemoved ticketRemoved)
        {
            Deleted = ticketRemoved.Deleted;
        }

        public void When(TicketUpdated ticketUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new TicketRemoved(deleted));
        }

        public void Update()
        {
            Apply(new TicketUpdated());
        }
    }
}
