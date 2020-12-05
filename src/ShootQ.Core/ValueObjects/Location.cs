using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShootQ.Core.ValueObjects
{
    public class Location : ValueObject
    {
        [JsonProperty]
        public double Longitude { get; private set; }
        [JsonProperty]
        public double Latitude { get; private set; }
        [JsonProperty]
        public string Street { get; private set; }
        [JsonProperty]
        public string City { get; private set; }
        [JsonProperty]
        public string Province { get; private set; }
        [JsonProperty]
        public string PostalCode { get; private set; }
        [JsonProperty]
        public Point Point { get; private set; }

        protected Location()
        {

        }

        private Location(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
        }

        public static Result<Location> Create(double longitude, double latitude)
        {
            return Result.Success(new Location(longitude, latitude));
        }
    }
}
