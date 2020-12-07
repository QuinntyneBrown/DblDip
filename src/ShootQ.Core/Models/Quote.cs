using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootQ.Core.Models
{
    public abstract class Quote : AggregateRoot
    {

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
        public Email ClientEmail { get; set; }
        public Price Total => (Price)LineItems.Sum(x => x.Amount);
        public ICollection<LineItem> LineItems { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? Declined { get; set; }
        public DateTime? Accepted { get; set; }

    }
}
