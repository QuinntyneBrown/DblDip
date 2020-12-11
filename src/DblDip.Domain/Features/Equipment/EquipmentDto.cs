using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Equipment
{
    public class EquipmentDto
    {
        public Guid EquipmentId { get; set; }
        public string Name { get; set; }
        public Price Price { get; set; }
        public string Description { get; set; }
        public Guid? ReceiptDigitalAssetId { get; set; }
    }
}
