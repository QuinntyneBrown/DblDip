using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Availability : AggregateRoot
    {
        public Availability()
        {
            Apply(new AvailabilityCreated(Guid.NewGuid()));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(AvailabilityCreated availabilityCreated)
        {
            AvailabilityId = availabilityCreated.AvailabilityId;
        }

        public void When(AvailabilityRemoved availabilityRemoved)
        {
            Deleted = availabilityRemoved.Deleted;
        }

        public void When(AvailabilityUpdated availabilityUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new AvailabilityRemoved(deleted));
        }

        public void Update()
        {

        }

        public Guid AvailabilityId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
