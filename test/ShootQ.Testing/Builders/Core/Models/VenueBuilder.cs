using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class VenueBuilder
    {
        private Venue _venue;

        public static Venue WithDefaults()
        {
            return new Venue();
        }

        public VenueBuilder()
        {
            _venue = WithDefaults();
        }

        public Venue Build()
        {
            return _venue;
        }
    }
}
