using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Receipt: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Receipt()
        {

        }

        protected override void EnsureValidState()
        {

        }

        public Guid ReceiptId { get; private set; }
        public Guid DigitalAssetId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
