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
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
    }
}
