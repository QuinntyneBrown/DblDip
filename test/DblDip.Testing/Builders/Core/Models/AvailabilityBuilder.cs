using DblDip.Core.Models;

namespace DblDip.Testing.Builders
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
