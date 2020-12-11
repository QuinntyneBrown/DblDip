using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Venue: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid VenueId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
