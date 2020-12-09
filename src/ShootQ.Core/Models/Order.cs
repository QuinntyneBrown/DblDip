using BuildingBlocks.Abstractions;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Order : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid OrderId { get; private set; }
        public decimal Total { get; private set; }
        public ICollection<LineItem> LineItems { get; private set; }

        public static System.Threading.Tasks.Task<Order> CreateFromQuoteId(Guid quoteId, IAppDbContext context)
        {
            throw new NotImplementedException();
        }
    }
}
