using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class QuoteBuilder
    {
        private Quote _quote;

        public QuoteBuilder()
        {

        }

        public Quote Build()
        {
            return _quote;
        }
    }
}
