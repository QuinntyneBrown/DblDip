using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Receipt : AggregateRoot
    {
        public Receipt()
        {
            Apply(new ReceiptCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(ReceiptCreated receiptCreated)
        {
            ReceiptId = receiptCreated.ReceiptId;
        }

        public void When(ReceiptRemoved receiptRemoved)
        {
            Deleted = receiptRemoved.Deleted;
        }

        public void When(ReceiptUpdated receiptUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }
        public void Remove(DateTime deleted)
        {
            Apply(new ReceiptRemoved(deleted));
        }

        public void Update()
        {
            Apply(new ReceiptUpdated());
        }

        public Guid ReceiptId { get; private set; }
        public Guid DigitalAssetId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
