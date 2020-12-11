using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Contact : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid ContactId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
