using BuildingBlocks.Abstractions;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Equipment : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid EquipmentId { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public string Description { get; private set; }
        public Guid? ReceiptDigitalAssetId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
