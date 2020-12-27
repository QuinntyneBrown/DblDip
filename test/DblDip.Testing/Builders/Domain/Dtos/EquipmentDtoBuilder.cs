using DblDip.Domain.Features.Equipment;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class EquipmentDtoBuilder
    {
        private EquipmentDto _equipmentDto;

        public static EquipmentDto WithDefaults()
        {
            return new(default, default, default, default, default);
        }

        public EquipmentDtoBuilder()
        {
            _equipmentDto = WithDefaults();
        }

        public EquipmentDto Build()
        {
            return _equipmentDto;
        }
    }
}
