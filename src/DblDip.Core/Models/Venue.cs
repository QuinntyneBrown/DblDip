using BuildingBlocks.Abstractions;
using System;
using DblDip.Core.ValueObjects;

namespace DblDip.Core.Models
{
    public class Venue : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid VenueId { get; private set; }

        public string Name { get; private set; }
        public Location Location { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
