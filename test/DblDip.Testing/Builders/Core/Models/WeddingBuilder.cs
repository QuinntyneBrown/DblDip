using DblDip.Core.Models;
using System;
using static DblDip.Testing.Factories.ConfigurationFactory;

namespace DblDip.Testing.Builders
{
    public class WeddingBuilder
    {
        private Wedding _wedding;

        public static Wedding WithDefaults(Rate photographyRate)
        {
            var configuration = Create();
            var longitude = Convert.ToDouble(configuration["DefaultLocation:Longitude"]);

            var latitude = Convert.ToDouble(configuration["DefaultLocation:Latitude"]);

            var defaultLocation = DblDip.Core.ValueObjects.Location.Create(longitude, latitude).Value;

            return new Wedding(defaultLocation, defaultLocation, defaultLocation, DateTime.UtcNow, 5);
        }

        public WeddingBuilder()
        {
            throw new NotImplementedException();
        }

        public Wedding Build()
        {
            return _wedding;
        }
    }
}
