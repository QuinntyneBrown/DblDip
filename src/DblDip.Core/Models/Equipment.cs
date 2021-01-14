using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Equipment : AggregateRoot
    {
        public Guid EquipmentId { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public string Description { get; private set; }
        public Guid? ReceiptDigitalAssetId { get; private set; }
        public DateTime? Deleted { get; private set; }
        protected Equipment()
        {

        }

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

        public void When(EquipmentUpdated equipmentUpdated)
        {
  
        }

        public void When(EquipmentRemoved equipmentRemoved)
        {
            Deleted = equipmentRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {
            Apply(new EquipmentUpdated());
        }

        public void Remove(DateTime deleted)
        {
            Apply(new EquipmentRemoved(deleted));
        }

    }
}
