using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class WeddingQuote : Quote
    {
        protected override void When(dynamic @event) => When(@event);

        public WeddingQuote(Email email, Wedding wedding, Rate rate)
        {
            Apply(new WeddingQuoteCreated(Guid.NewGuid(), email, wedding.WeddingId));

            foreach (var part in wedding.Parts)
            {
                AddItem((Price)(part.DateRange.Hours * rate.Price), "Wedding Photography");
            }

            foreach (var trip in wedding.Trips)
            {
                AddItem((Price)(trip.DateRange.Hours * 60), "Travel Expense");
            }
        }

        protected override void EnsureValidState()
        {

        }

        public void When(WeddingQuoteCreated weddingQuoteCreated)
        {
            WeddingQuoteId = weddingQuoteCreated.WeddingQuoteId;
            LineItems = new List<LineItem>();
            ClientEmail = weddingQuoteCreated.Email;
            WeddingId = weddingQuoteCreated.WeddingId;
        }

        public Guid WeddingQuoteId { get; set; }
        public Guid WeddingId { get; private set; }
    }
}
