using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootQ.Core.Models
{
    public class Wedding : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Wedding(Location start, Location end, Location location, DateTime dateTime, int hours)
        {
            Apply(new WeddingCreated(start, end, location, Guid.NewGuid(), dateTime, hours));
        }
        public void When(WeddingCreated weddingCreated)
        {
            WeddingId = weddingCreated.WeddingId;
            Start = weddingCreated.Start;
            End = weddingCreated.End;
            Parts = new List<WeddingPart>();
            WeddingQuotes = new List<WeddingQuote>();
            Trips = new List<Trip>();

            var dateRange = DateRange.Create(weddingCreated.DateTime, weddingCreated.DateTime.AddHours(weddingCreated.Hours));

            Parts.Add(new WeddingPart(dateRange.Value, null, null));
        }

        public void When(WeddingPartAdded weddingPartAdded)
        {
            var dateRange = DateRange.Create(weddingPartAdded.DateTime, weddingPartAdded.DateTime.AddHours(weddingPartAdded.Hours));

            Parts.Add(new WeddingPart(dateRange.Value, weddingPartAdded.Location, weddingPartAdded.Description));
        }

        public Wedding AddPart(DateTime dateTime, int hours, Price rate, Location destination, string description)
        {
            Apply(new WeddingPartAdded(dateTime, hours, destination, description));

            return this;
        }

        protected override void EnsureValidState()
        {
            if (Parts == null)
                throw new Exception("Model Invalid. Parts can not be null.");

            foreach (var part in Parts)
            {
                if (Parts.Any(x => (x.DateRange.Overlap(part.DateRange) && x != part)))
                {
                    throw new Exception("Model Invalid. Parts overlap");
                }
            }
        }

        public Guid WeddingId { get; private set; }
        public Location Start { get; private set; }
        public Location End { get; private set; }
        public ICollection<WeddingPart> Parts { get; private set; }
        public ICollection<WeddingQuote> WeddingQuotes { get; private set; }
        public ICollection<Trip> Trips { get; private set; }
        public IDictionary<DateRange, dynamic> Schedule
        {
            get
            {
                var schedule = new Dictionary<DateRange, dynamic>();

                foreach (var trip in Trips)
                {
                    schedule.Add(trip.DateRange, trip);
                }

                foreach (var part in Parts)
                {
                    schedule.Add(part.DateRange, part);
                }

                schedule.OrderBy(x => x.Key);

                return schedule;
            }
        }
    }

    public record WeddingPart(DateRange DateRange, Location Location, string Description);

    public record Trip(DateRange DateRange, Location Start, Location End);
}
