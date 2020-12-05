using BuildingBlocks.Abstractions;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Quote : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid QuoteId { get; private set; }

        public Price Total { get; set; }

        public record LineItem(Price Amount, string Description);
    }
}
