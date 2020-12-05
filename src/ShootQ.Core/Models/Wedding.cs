using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Wedding: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Wedding(Guid customerId, DateTime dateTime, int hours, Guid weddingRateId)
        {
            Apply(new WeddingCreated(Guid.NewGuid(), dateTime, hours, customerId, weddingRateId));
        }
        public void When(WeddingCreated weddingCreated)
        {
            WeddingId = weddingCreated.WeddingId;
            CustomerId = weddingCreated.CustomerId;
            WeddingRateId = weddingCreated.WeddingRateId;

            Parts = new List<WeddingPart>();

            var dateRange = DateRange.Create(weddingCreated.DateTime, weddingCreated.DateTime.AddHours(weddingCreated.Hours));

            Parts.Add(new WeddingPart(dateRange.Value, null, null));
        }

        public void When(WeddingPartAdded weddingPartAdded)
        {
            var dateRange = DateRange.Create(weddingPartAdded.DateTime, weddingPartAdded.DateTime.AddHours(weddingPartAdded.Hours));

            Parts.Add(new WeddingPart(dateRange.Value, weddingPartAdded.Location, weddingPartAdded.Description));
        }

        public Wedding AddPart(DateTime dateTime, int hours, Location location, string description)
        {
            Apply(new WeddingPartAdded(dateTime, hours, location, description));

            return this;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid WeddingId { get; private set; }
        public Guid CustomerId { get; set; }
        public Guid WeddingRateId { get; set; }
        public ICollection<WeddingPart> Parts { get; private set; }
    }

    public record WeddingPart(DateRange dateRange, Location location, string Description);
}
