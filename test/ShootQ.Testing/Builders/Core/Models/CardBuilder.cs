using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class CardBuilder
    {
        private Card _card;

        public CardBuilder(string name)
        {
            _card = new Card(name);
        }

        public Card Build()
        {
            return _card;
        }
    }
}
