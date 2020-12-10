using BuildingBlocks.Abstractions;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Photographer : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid PhotographerId { get; private set; }
        public string Name { get; set; }
        public Email Email { get; set; }
        public string PhoneNumber { get; set; }
        public Location PrimaryLocation { get; set; }
    }
}
