using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Quote : AggregateRoot
    {
        protected override void When(dynamic @event)
        {
            if (@event is QuoteCreated || @event is QuoteItemAdded)
            {
                When(@event);
            }
        }

        public Quote(Guid quoteId)
        {
            Apply(new QuoteCreated(quoteId));
        }

        public void When(QuoteCreated quoteCreated)
        {
            QuoteId = quoteCreated.QuoteId;
            LineItems = new List<LineItem>();
        }

        public void When(QuoteItemAdded quoteItemAdded)
        {
            LineItems.Add(LineItem.Create(quoteItemAdded.Amount, quoteItemAdded.Description).Value);
        }

        public void When(QuoteCompleted quoteCompleted)
        {
            Completed = quoteCompleted.Completed;
        }

        public void When(QuoteDeclined quoteDeclined)
        {
            Declined = quoteDeclined.Declined;
        }

        public void When(QuoteAccepted quoteAccepted)
        {
            Accepted = quoteAccepted.Accepted;
        }

        protected override void EnsureValidState()
        {

        }

        public void AddItem(Price amount, string description)
        {
            Apply(new QuoteItemAdded(amount, description));
        }

        public void Complete(DateTime completed)
        {
            Apply(new QuoteCompleted(completed));
        }

        public void Decline(DateTime declined)
        {
            Apply(new QuoteDeclined(declined));
        }

        public void Accept(DateTime accepted)
        {
            Apply(new QuoteAccepted(accepted));
        }

        public Guid QuoteId { get; protected set; }
        public Guid VendorId { get; private set; }
        public Email BillToEmail { get; protected set; }
        public Price Total => (Price)LineItems.Sum(x => x.Amount);
        public ICollection<LineItem> LineItems { get; protected set; }
        public DateTime? Completed { get; protected set; }
        public DateTime? Declined { get; protected set; }
        public DateTime? Accepted { get; protected set; }

    }
}
