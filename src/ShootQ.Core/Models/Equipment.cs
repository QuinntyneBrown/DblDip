using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Equipment : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid EquipmentId { get; private set; }
        public string Name { get; set; }
        decimal Price { get; set; }
        public string Description { get; set; }
        public Guid? ReceiptDigitalAssetId { get; set; }
    }
}
