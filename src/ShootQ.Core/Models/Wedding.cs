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

        public Wedding(Guid customerId, DateTime dateTime, int hours, Guid photographyRateId)
        {
            Apply(new WeddingCreated(Guid.NewGuid(), dateTime, hours, customerId, photographyRateId));
        }
        public void When(WeddingCreated weddingCreated)
        {
            WeddingId = weddingCreated.WeddingId;
            CustomerId = weddingCreated.CustomerId;
            
            Parts = new List<WeddingPart>();

            var dateRange = DateRange.Create(weddingCreated.DateTime, weddingCreated.DateTime.AddHours(weddingCreated.Hours));

            Parts.Add(new WeddingPart(dateRange.Value, null, weddingCreated.PhotographyRateId, null));
        }

        public void When(WeddingPartAdded weddingPartAdded)
        {
            var dateRange = DateRange.Create(weddingPartAdded.DateTime, weddingPartAdded.DateTime.AddHours(weddingPartAdded.Hours));

            Parts.Add(new WeddingPart(dateRange.Value, weddingPartAdded.Location, weddingPartAdded.PhotographyRateId, weddingPartAdded.Description));
        }

        public Wedding AddPart(DateTime dateTime, int hours, Guid photographyRateId, Location location, string description)
        {
            Apply(new WeddingPartAdded(dateTime, hours, location, photographyRateId, description));

            return this;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid WeddingId { get; private set; }
        public Guid CustomerId { get; set; }
        public ICollection<WeddingPart> Parts { get; private set; }
    }

    public record WeddingPart(DateRange dateRange, Location location, Guid PhotographyRateId, string Description);
}
