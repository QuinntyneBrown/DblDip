using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.ValueObjects
{
    [Owned]
    public class PhoneNumber : ValueObject
    {

        [JsonProperty]
        public string Value { get; private set; }

        protected PhoneNumber()
        {

        }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static Result<PhoneNumber> Create(string value)
        {
            value = (value ?? string.Empty).Trim();

            if (value.Length == 0)
                return Result.Failure<PhoneNumber>("PhoneNumber should not be empty.");

            return Result.Success(new PhoneNumber(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(PhoneNumber phoneNumber)
        {
            return phoneNumber.Value;
        }

        public static explicit operator PhoneNumber(string phoneNumber)
        {
            return Create(phoneNumber).Value;
        }
    }
}
