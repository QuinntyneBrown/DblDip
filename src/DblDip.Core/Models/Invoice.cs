using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Invoice : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Invoice()
        {
            Apply(new InvoiceCreated(Guid.NewGuid()));
        }
        public void When(InvoiceCreated invoiceCreated)
        {
            InvoiceId = invoiceCreated.InvoiceId;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid InvoiceId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
