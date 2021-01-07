using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Offer : AggregateRoot
    {
        public Guid OfferId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public DateTime Expires { get; private set; }
        public DateTime? Deleted { get; private set; }

        public Offer(IEnumerable<object> events)
            : base(events) { }

        public Offer()
        {
            Apply(new OfferCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(OfferCreated offerCreated)
        {
            OfferId = offerCreated.OfferId;
        }

        public void When(OfferRemoved offerRemoved)
        {
            Deleted = offerRemoved.Deleted;
        }

        public void When(OfferUpdated offerUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new OfferRemoved(deleted));
        }

        public void Update()
        {
            Apply(new OfferUpdated());
        }


    }
}
