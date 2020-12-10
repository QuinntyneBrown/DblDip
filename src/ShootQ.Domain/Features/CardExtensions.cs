using ShootQ.Core.Models;
using ShootQ.Domain.Features.Cards;

namespace ShootQ.Domain.Features
{
    public static class CardExtensions
    {
        public static CardDto ToDto(this Card card)
            => new CardDto(card.CardId, card.Name, card.Description);
    }
}
