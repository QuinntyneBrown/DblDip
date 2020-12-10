using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders
{
    public class LeadBuilder
    {
        private Lead _lead;

        public static Lead WithDefaults() => new Lead();
        public LeadBuilder()
        {
            _lead = new Lead();
        }

        public Lead Build()
        {
            return _lead;
        }
    }
}
