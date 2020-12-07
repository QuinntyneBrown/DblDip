using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Rate : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Rate(string name, Price price, Guid rateId = default)
        {
            Apply(new RateCreated(rateId == default ? Guid.NewGuid() : rateId, price, name));
        }
        public void When(RateCreated photographyRateCreated)
        {
            RateId = photographyRateCreated.PhotographyRateId;
            Price = photographyRateCreated.Price;
            Name = photographyRateCreated.Name;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid RateId { get; private set; }
        public string Name { get; set; }
        public Price Price { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
