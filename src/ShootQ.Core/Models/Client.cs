using BuildingBlocks.Abstractions;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Client : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Client CreateFrom(Lead lead)
        {
            throw new NotImplementedException("");
        }

        public Guid ClientId { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public Email Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
