using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Customer: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid CustomerId { get; private set; }
    }
}
