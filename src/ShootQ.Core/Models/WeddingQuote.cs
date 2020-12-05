using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class WeddingQuote: Quote
    {
        protected override void When(dynamic @event) => When(@event);

        public WeddingQuote(Email email, Guid weddingId)
        {
            Apply(new WeddingQuoteCreated(Guid.NewGuid(), email, weddingId));
        }

        protected override void EnsureValidState()
        {

        }

        public void When(WeddingQuoteCreated weddingQuoteCreated)
        {
            WeddingQuoteId = weddingQuoteCreated.WeddingQuoteId;
            LineItems = new List<LineItem>();
            Email = weddingQuoteCreated.Email;
            WeddingId = weddingQuoteCreated.WeddingId;
        }

        public Guid WeddingQuoteId { get; set; }
        public Guid WeddingId { get; private set; }
    }
}
