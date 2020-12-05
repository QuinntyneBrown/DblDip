using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShootQ.Core.ValueObjects
{
    public class Location : ValueObject
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public string Value { get; private set; }

        protected Location()
        {

        }

        private Location(string value)
        {
            Value = value;
        }

        public static Result<Location> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<Location>("Location should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<Location>("Location name is too long.");

            return Result.Success(new Location(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Location location)
        {
            return location.Value;
        }

        public static explicit operator Location(string location)
        {
            return Create(location).Value;
        }
    }
}
