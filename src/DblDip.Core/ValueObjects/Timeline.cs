using BuildingBlocks.Core;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using DblDip.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DblDip.Core.ValueObjects
{
    [Owned]
    public class Timeline : ValueObject, IScheduled
    {
        public const int MaxLength = 250;
        [JsonProperty]
        public ICollection<IScheduled> Value { get; private set; }

        public DateRange Scheduled
        {
            get
            {
                var orderedValue = Value.OrderBy(x => x.Scheduled).Select(x => x.Scheduled);

                return DateRange.Create(orderedValue.First().StartDate, orderedValue.Last().StartDate).Value;
            }
        }

        protected Timeline()
        {

        }

        private Timeline(ICollection<IScheduled> scheduledItems)
        {
            Value = scheduledItems;
        }

        public static Result<Timeline> Create(List<IScheduled> scheduledItems)
        {
            Guard.ArgumentNotNull(nameof(scheduledItems), scheduledItems);

            return Result.Success(new Timeline(scheduledItems));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

    }
}
