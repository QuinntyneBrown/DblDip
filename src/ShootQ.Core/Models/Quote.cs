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

        public void AddItem(Price amount, string description)
        {
            Apply(new QuoteItemAdded(amount,description));
        }

        public Guid QuoteId { get; protected set; }
        public Email Email { get; set; }
        public Price Total => (Price)LineItems.Sum(x => x.Amount);
        public ICollection<LineItem> LineItems { get; set; }

    }
}
