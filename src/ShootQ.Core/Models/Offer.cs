using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Offer: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid OfferId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public DateTime Expires { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
