using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Rate : AggregateRoot
    {
        public Guid RateId { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public DateTime? Deleted { get; private set; }
        protected Rate()
        {

        }

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

        public void When(RateUpdated rateUpdated)
        {

        }

        public void When(RateRemoved rateRemoved)
        {
            Deleted = rateRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new RateRemoved(deleted));
        }
    }
}
