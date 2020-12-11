using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class LeadBuilder
    {
        private Lead _lead;

        public static Lead WithDefaults()
        {
            return new Lead();
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
