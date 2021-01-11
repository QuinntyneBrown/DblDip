using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.ValueObjects
{
    [Owned]
    public class LineItem : ValueObject
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public Price Amount { get; private set; }
        public string Description { get; private set; }

        protected LineItem()
        {

        }

        private LineItem(Price amount, string description)
        {
            Amount = amount;
            Description = description;
        }

        public static Result<LineItem> Create(Price amount, string description)
        {
            description = (description ?? string.Empty).Trim();

            if (description.Length == 0)
                return Result.Failure<LineItem>("LineItem should not be empty.");

            if (description.Length > MaxLength)
                return Result.Failure<LineItem>("LineItem name is too long.");

            return Result.Success(new LineItem(amount, description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
            yield return Amount;
        }
    }
}
