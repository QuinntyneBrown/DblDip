using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DblDip.Core.ValueObjects
{
    public class StoryPoints : ValueObject
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public int Value { get; private set; }

        protected StoryPoints()
        {

        }

        private StoryPoints(int value)
        {
            Value = value;
        }

        public static Result<StoryPoints> Create(int value)
        {
            //make sure it's fibonacci

            if (value == default)
                return Result.Failure<StoryPoints>("StoryPoints invalid");

            return Result.Success(new StoryPoints(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(StoryPoints storyPoints)
        {
            return storyPoints.Value;
        }

        public static explicit operator StoryPoints(int storyPoints)
        {
            return Create(storyPoints).Value;
        }
    }
}
