using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.Collections.Generic;
using BuildingBlocks.GeoLocation;
using NetTopologySuite;

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

        public double Distance(Location location)
            => Point.ProjectTo(2855).Distance(location.Point.ProjectTo(2855)) / 1000;

        private Location(double longitude, double latitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            Longitude = longitude;
            Latitude = latitude;
            Point = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
        }

        public static Result<Location> Create(double longitude, double latitude)
        {
            return CSharpFunctionalExtensions.Result.Success(new Location(longitude, latitude));
        }
    }
}
