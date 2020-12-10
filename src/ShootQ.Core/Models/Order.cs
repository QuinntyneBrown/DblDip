using BuildingBlocks.Abstractions;
using BuildingBlocks.Stripe;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Order : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public void When(OrderCheckedOut orderCheckedOut)
        {

        }

        public void When(OrderPaid paid)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Checkout(IPaymentProcessor paymentProcessor)
        {
            Apply(new OrderCheckedOut());

            Apply(new OrderPaid());
        }

        public Guid OrderId { get; private set; }
        public Price Total { get; private set; }
        public Email BillToEmail { get; private set; }
        public ICollection<LineItem> LineItems { get; private set; }
        public OrderStatus Status { get; private set; }

        public static System.Threading.Tasks.Task<Order> FromQuote(Quote quote)
        {
            throw new NotImplementedException();
        }
    }

    public enum OrderStatus
    {
        New,
        Paid
    }
}
