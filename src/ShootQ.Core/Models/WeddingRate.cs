using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class WeddingRate: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public WeddingRate(Price price)
        {
            Apply(new WeddingRateCreated(Guid.NewGuid(), price));
        }
        public void When(WeddingRateCreated weddingRateCreated)
        {
            WeddingRateId = weddingRateCreated.WeddingRateId;
            Price = weddingRateCreated.Price;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid WeddingRateId { get; private set; }
        public Price Price { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
