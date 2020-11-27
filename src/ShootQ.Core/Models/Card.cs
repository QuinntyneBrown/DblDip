using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class Card : AggregateRoot
    {
        public Card(string name)
        {
            Apply(new CardCreated(name, Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(CardCreated cardCreated)
        {
            CardId = cardCreated.CardId;
            Name = cardCreated.Name;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid CardId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
