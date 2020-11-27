using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Address : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid AddressId { get; private set; }
    }
}
