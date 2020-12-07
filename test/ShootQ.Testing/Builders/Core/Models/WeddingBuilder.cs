using ShootQ.Core.Models;
using System;
using static ShootQ.Testing.Factories.ConfigurationFactory;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class WeddingBuilder
    {
        private Wedding _wedding;

        public static Wedding WithDefaults(Rate photographyRate)
        {
            var configuration = Create();
            var longitude = Convert.ToDouble(configuration["DefaultLocation:Longitude"]);

            var latitude = Convert.ToDouble(configuration["DefaultLocation:Latitude"]);

            var defaultLocation = ShootQ.Core.ValueObjects.Location.Create(longitude, latitude).Value;

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
