using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Card : AggregateRoot
    {
        public Card(string name, string description)
        {
            Apply(new CardCreated(Guid.NewGuid(), name, description));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(CardCreated cardCreated)
        {
            CardId = cardCreated.CardId;
            Name = cardCreated.Name;
            Description = cardCreated.Description;
        }

        public void When(CardRemoved cardRemoved)
        {
            Deleted = cardRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime removed)
        {
            Apply(new CardRemoved(removed));
        }

        public Guid CardId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
