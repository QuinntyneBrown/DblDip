using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class EquipmentExtensions
    {
        public static EquipmentDto ToDto(this DblDip.Core.Models.Equipment equipment)
            => new(default, default, default, default, default);
    }
}
