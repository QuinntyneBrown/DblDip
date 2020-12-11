using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShootQ.Core.ValueObjects
{
    public class Url : ValueObject
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public string Value { get; private set; }

        protected Url()
        {

        }

        private Url(string value)
        {
            Value = value;
        }

        public static Result<Url> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<Url>("Url should not be empty.");

            if (value.Length > MaxLength)
                return Result.Failure<Url>("Url name is too long.");

            return Result.Success(new Url(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Url url)
        {
            return url.Value;
        }

        public static explicit operator Url(string url)
        {
            return Create(url).Value;
        }
    }
}
