using BuildingBlocks.Abstractions;
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

        public record LineItem
        {

        }
    }
}
