using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Order: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid OrderId { get; private set; }
        public decimal Total { get; set; }

        public ICollection<LineItem> LineItems { get; set; }

        public record LineItem
        {

        }
    }
}
