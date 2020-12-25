using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Equipment
{
    public class EquipmentDto
    {
        public Guid EquipmentId { get; init; }
        public string Name { get; init; }
        public Price Price { get; init; }
        public string Description { get; init; }
        public Guid? ReceiptDigitalAssetId { get; init; }
    }
}
