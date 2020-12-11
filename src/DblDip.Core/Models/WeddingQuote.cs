using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class WeddingQuote : Quote
    {
        protected override void When(dynamic @event) => When(@event);

        public WeddingQuote(Email billToEmail, Wedding wedding, Rate rate)
        {
            Apply(new WeddingQuoteCreated(Guid.NewGuid(), billToEmail, wedding.WeddingId));

            foreach (var part in wedding.Parts)
            {
                AddItem((Price)(part.Scheduled.Hours * rate.Price), "Wedding Photography");
            }

            foreach (var trip in wedding.Trips)
            {
                AddItem((Price)(trip.Scheduled.Hours * 60), "Travel Expense");
            }
        }

        protected override void EnsureValidState()
        {

        }

        public void When(WeddingQuoteCreated weddingQuoteCreated)
        {
            WeddingQuoteId = weddingQuoteCreated.WeddingQuoteId;
            LineItems = new List<LineItem>();
            BillToEmail = weddingQuoteCreated.BillToEmail;
            WeddingId = weddingQuoteCreated.WeddingId;
        }

        public Guid WeddingQuoteId { get; private set; }
        public Guid WeddingId { get; private set; }
    }
}
