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
        public string Street { get; private set; }
        public string PostalCode { get; private set; }
        public string Province { get; private set; }
    }
}
