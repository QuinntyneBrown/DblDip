using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class QuoteBuilder
    {
        private Quote _quote;

        public QuoteBuilder()
        {
            _quote = new Quote();
        }

        public Quote Build()
        {
            return _quote;
        }
    }
}
