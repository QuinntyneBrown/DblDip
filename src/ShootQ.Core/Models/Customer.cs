using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Customer : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Customer()
        {
            Apply(new CustomerCreated(Guid.NewGuid()));
        }
        public void When(CustomerCreated customerCreated)
        {
            CustomerId = customerCreated.CustomerId;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid CustomerId { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Email EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
