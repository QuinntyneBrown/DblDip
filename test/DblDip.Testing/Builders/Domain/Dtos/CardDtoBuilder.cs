using DblDip.Domain.Features;
using System;

namespace DblDip.Testing.Builders
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
