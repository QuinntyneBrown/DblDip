using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Equipment
{
    public class EquipmentDto
    {
        public EquipmentDto(Guid equipmentId, string name, Price price, string description, Guid? receiptDigitalAssetId)
        {
            EquipmentId = equipmentId;
            Name = name;
            Price = price;
            Description = description;
            ReceiptDigitalAssetId = receiptDigitalAssetId;
        }

        public Guid EquipmentId { get; init; }
        public string Name { get; init; }
        public Price Price { get; init; }
        public string Description { get; init; }
        public Guid? ReceiptDigitalAssetId { get; init; }
    }
}
