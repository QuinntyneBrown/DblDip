using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class PaymentSchedule: AggregateRoot
    {
        public Guid PaymentScheduleId { get; private set; }
        public DateTime? Deleted { get; private set; }
        public PaymentSchedule()
        {
            Apply(new PaymentScheduleCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(PaymentScheduleCreated paymentScheduleCreated)
        {
            PaymentScheduleId = paymentScheduleCreated.PaymentScheduleId;
        }

        public void When(PaymentScheduleUpdated paymentScheduleUpdated)
        {

        }

        public void When(PaymentScheduleRemoved paymentScheduleRemoved)
        {
            Deleted = paymentScheduleRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new PaymentScheduleRemoved(deleted));
        }
    }
}
