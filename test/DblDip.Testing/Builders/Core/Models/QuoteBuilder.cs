using DblDip.Core.Models;
using DblDip.Core.ValueObjects;

namespace DblDip.Testing.Builders.Core.Models
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
