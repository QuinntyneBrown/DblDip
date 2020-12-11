using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class AvailabilityBuilder
    {
        private Availability _availability;

        public static Availability WithDefaults()
        {
            return new Availability();
        }

        public AvailabilityBuilder()
        {
            _availability = WithDefaults();
        }

        public Availability Build()
        {
            return _availability;
        }
    }
}
