using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootQ.Core.Models
{
    public class Wedding: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Wedding(Location location, DateTime dateTime, int hours, Guid photographyRateId)
        {
            Apply(new WeddingCreated(location, Guid.NewGuid(), dateTime, hours, photographyRateId));
        }
        public void When(WeddingCreated weddingCreated)
        {
            WeddingId = weddingCreated.WeddingId;            
            Parts = new List<WeddingPart>();
            WeddingQuotes = new List<WeddingQuote>();

            var dateRange = DateRange.Create(weddingCreated.DateTime, weddingCreated.DateTime.AddHours(weddingCreated.Hours));

            Parts.Add(new WeddingPart(weddingCreated.Location, dateRange.Value, null, weddingCreated.PhotographyRateId, null));
        }

        public void When(WeddingPartAdded weddingPartAdded)
        {
            var dateRange = DateRange.Create(weddingPartAdded.DateTime, weddingPartAdded.DateTime.AddHours(weddingPartAdded.Hours));

            Parts.Add(new WeddingPart(weddingPartAdded.Location, dateRange.Value, weddingPartAdded.Location, weddingPartAdded.PhotographyRateId, weddingPartAdded.Description));
        }

        public void When(WeddingQuoteCreated weddingQuoteCreated)
        {
            WeddingQuotes.Add(new WeddingQuote(weddingQuoteCreated.Email, weddingQuoteCreated.Total, weddingQuoteCreated.Created));
        }

        public Wedding AddPart(DateTime dateTime, int hours, Guid photographyRateId, Location location, string description)
        {
            Apply(new WeddingPartAdded(dateTime, hours, location, photographyRateId, description));

            return this;
        }

        public void AddQuote(Email email, Price total, DateTime created)
        {
            Apply(new WeddingQuoteCreated(email, total, created));
        }

        protected override void EnsureValidState()
        {
            if (Parts == null)
                throw new Exception("Model Invalid. Parts can not be null.");

            foreach(var part in Parts)
            {
                if(Parts.Any(x => (x.DateRange.Overlap(part.DateRange) && x != part)))
                {
                    throw new Exception("Model Invalid. Parts overlap");
                }
            }
        }

        public Guid WeddingId { get; private set; }
        public ICollection<WeddingPart> Parts { get; private set; }
        public ICollection<WeddingQuote> WeddingQuotes { get; private set; }
    }

    public record WeddingPart(Location location, DateRange DateRange, Location Location, Guid PhotographyRateId, string Description);

    public record WeddingQuote(Email Email, Price Total, DateTime Created);
}
