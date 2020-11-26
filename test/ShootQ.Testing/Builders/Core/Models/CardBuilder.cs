using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class CardBuilder
    {
        private Card _card;

        public CardBuilder()
        {
            _card = new Card();
        }

        public Card Build()
        {
            return _card;
        }
    }
}
