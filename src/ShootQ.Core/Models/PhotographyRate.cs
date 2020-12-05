using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class PhotographyRate: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public PhotographyRate(Price price)
        {
            Apply(new PhotographyRateCreated(Guid.NewGuid(), price));
        }
        public void When(PhotographyRateCreated photographyRateCreated)
        {
            PhotographyRateId = photographyRateCreated.PhotographyRateId;
            Price = photographyRateCreated.Price;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid PhotographyRateId { get; private set; }
        public Price Price { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
