using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class SystemLocation: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public SystemLocation(string name, Location location)
        {
            Apply(new SystemLocationCreated(Guid.NewGuid(), name, location));
        }
        public void When(SystemLocationCreated systemLocationCreated)
        {
            SystemLocationId = systemLocationCreated.SystemLocationId;
            Name = systemLocationCreated.Name;
            Location = systemLocationCreated.Location;
        }

        public void When(SystemLocationRemoved systemLocationRemoved)
        {
            Deleted = systemLocationRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new SystemLocationRemoved(deleted));
        }

        public Guid SystemLocationId { get; private set; }
        public string Name { get; private set; }
        public Location Location { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
