using System;

namespace ShootQ.Core.DomainEvents
{
    public class CardCreated
    {
        public CardCreated(string name, Guid cardId)
        {
            CardId = cardId;
            Name = name;
        }

        public Guid CardId { get; set; }
        public string Name { get; }
    }
}
