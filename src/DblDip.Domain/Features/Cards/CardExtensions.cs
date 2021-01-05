using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class CardExtensions
    {
        public static CardDto ToDto(this Card card)
            => new CardDto(card.CardId, card.Name, card.Description);
    }
}
