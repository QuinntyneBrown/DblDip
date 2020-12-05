using System;
using ShootQ.Core.ValueObjects;
using static ShootQ.Testing.Factories.ConfigurationFactory;

namespace ShootQ.Testing.Builders.Core.ValueObjects
{
    public class LocationBuilder
    {
        private ShootQ.Core.ValueObjects.Location _location;

        public static ShootQ.Core.ValueObjects.Location WithDefaults()
        {
            var configuration = Create();

            var longitude = Convert.ToDouble(configuration["DefaultLocation:Longitude"]);

            var latitude = Convert.ToDouble(configuration["DefaultLocation:Latitude"]);

            return ShootQ.Core.ValueObjects.Location.Create(longitude,latitude).Value;
        }

        public LocationBuilder()
        {
            throw new NotImplementedException();
        }

        public Location Build()
        {
            return _location;
        }
    }
}
