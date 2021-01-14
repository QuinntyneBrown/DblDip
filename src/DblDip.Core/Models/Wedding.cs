using BuildingBlocks.Core;
using DblDip.Core.DomainEvents;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Wedding : PhotographyProject
    {
        public Guid WeddingId { get; private set; }
        public Location StartLocation { get; private set; }
        public Location EndLocation { get; private set; }
        public ICollection<WeddingPart> Parts { get; private set; }
        public ICollection<Trip> Trips { get; private set; }
        public Timeline Timeline
            => Timeline.Create(new List<IScheduled>()
            .Concat(Trips)
            .Concat(Parts)
            .ToList()).Value;

        public override DateRange Scheduled => Timeline.Scheduled;
        protected Wedding()
        {

        }
        protected override void When(dynamic @event) => When(@event);

        public Wedding(Location start, Location end, Location location, DateTime dateTime, int hours)
        {
            Guard.ArgumentNotNull(nameof(start), start);
            Guard.ArgumentNotNull(nameof(end), end);
            Guard.ArgumentNotNull(nameof(location), location);
            Guard.ArgumentNotNull(nameof(dateTime), dateTime);
            Guard.ArgumentNotNull(nameof(hours), hours);

            Apply(new WeddingCreated(start, end, location, Guid.NewGuid(), dateTime, hours));
        }
        public void When(WeddingCreated weddingCreated)
        {
            WeddingId = weddingCreated.WeddingId;
            StartLocation = weddingCreated.Start;
            EndLocation = weddingCreated.End;
            Parts = new List<WeddingPart>();
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
                if (Parts.Any(x => (x.Scheduled.Overlap(part.Scheduled) && x != part)))
                {
                    throw new Exception("Model Invalid. Parts overlap");
                }
            }
        }

    }
}
