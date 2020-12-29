using BuildingBlocks.Abstractions;
using BuildingBlocks.Stripe;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
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

        public void When(OrderRemoved orderRemoved)
        {
            Deleted = orderRemoved.Deleted;
        }

        public void When(OrderUpdated orderUpdated)
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

        public void Remove(DateTime deleted)
        {
            Apply(new OrderRemoved(deleted));
        }

        public void Update()
        {
            Apply(new OrderUpdated());
        }

        public Guid OrderId { get; private set; }
        public Guid VendorId { get; private set; }
        public Price Total { get; private set; }
        public Email BillToEmail { get; private set; }
        public ICollection<LineItem> LineItems { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime? Deleted { get; private set; }


        public static System.Threading.Tasks.Task<Order> FromQuote(Quote quote)
        {
            throw new NotImplementedException();
        }
    }
}
