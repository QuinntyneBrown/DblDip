using BuildingBlocks.Abstractions;
using System;
using DblDip.Core.ValueObjects;
using DblDip.Core.DomainEvents;

namespace DblDip.Core.Models
{
    public class Venue : AggregateRoot
    {
        public Venue()
        {
            Apply(new VenueCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(VenueRemoved venueRemoved)
        {
            Deleted = venueRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new VenueRemoved(deleted));
        }

        public Guid VenueId { get; private set; }
        public string Name { get; private set; }
        public Location Location { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
