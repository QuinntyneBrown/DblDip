using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Equipment : AggregateRoot
    {
        public Equipment(string name, Price price, string description)
        {
            Apply(new EquipmentCreated(Guid.NewGuid(), name, price, description));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(EquipmentCreated equipmentCreated)
        {
            EquipmentId = equipmentCreated.EquipmentId;
            Name = equipmentCreated.Name;
            Price = equipmentCreated.Price;
            Description = equipmentCreated.Description;
        }

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
