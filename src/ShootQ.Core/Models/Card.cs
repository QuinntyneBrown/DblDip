using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Card: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid CardId { get; private set; }
        public string Name { get; set; }
    }
}
