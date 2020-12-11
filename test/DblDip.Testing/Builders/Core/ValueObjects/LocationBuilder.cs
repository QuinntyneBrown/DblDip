using System;
using DblDip.Core.ValueObjects;
using static DblDip.Testing.Factories.ConfigurationFactory;

namespace DblDip.Testing.Builders.Core.ValueObjects
{
    public class LocationBuilder
    {
        private DblDip.Core.ValueObjects.Location _location;

        public static DblDip.Core.ValueObjects.Location WithDefaults()
        {
            var configuration = Create();

            var longitude = Convert.ToDouble(configuration["DefaultLocation:Longitude"]);

            var latitude = Convert.ToDouble(configuration["DefaultLocation:Latitude"]);

            return DblDip.Core.ValueObjects.Location.Create(longitude, latitude).Value;
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
