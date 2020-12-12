using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class CardBuilder
    {
        private Card _card;

        public static Card WithDefaults() => new Card(default, "");
        public CardBuilder(string name)
        {
            _card = new Card(name, "");
        }

        public Card Build()
        {
            return _card;
        }
    }
}
