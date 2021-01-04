using DblDip.Core.Models;
using DblDip.Core.ValueObjects;

namespace DblDip.Testing.Builders
{
    public class LeadBuilder
    {
        private Lead _lead;

        public static Lead WithDefaults()
        {
            return new Lead((Email)"test@test.com");
        }

        public LeadBuilder()
        {
            _lead = WithDefaults();
        }

        public Lead Build()
        {
            return _lead;
        }
    }
}
