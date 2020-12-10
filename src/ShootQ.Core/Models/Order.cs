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
        public Price Total { get; private set; }
        public Email BillToEmail { get; private set; }
        public ICollection<LineItem> LineItems { get; private set; }

        public static System.Threading.Tasks.Task<Order> FromQuote(Quote quote)
        {
            throw new NotImplementedException();
        }
    }
}
