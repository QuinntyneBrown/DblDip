using ShootQ.Domain.Features.Cards;
using System;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class CardDtoBuilder
    {
        private CardDto _cardDto;

        public static CardDto WithDefaults()
        {
            return new CardDto(Guid.NewGuid(), "Test", null);
        }

        public CardDtoBuilder()
        {
            _cardDto = WithDefaults();
        }

        public CardDto Build()
        {
            return _cardDto;
        }
    }
}
