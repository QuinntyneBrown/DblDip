using DblDip.Domain.Features.Equipment;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class EquipmentDtoBuilder
    {
        private EquipmentDto _equipmentDto;

        public static EquipmentDto WithDefaults()
        {
            return new EquipmentDto();
        }

        public EquipmentDtoBuilder()
        {
            _equipmentDto = new EquipmentDto();
        }

        public EquipmentDto Build()
        {
            return _equipmentDto;
        }
    }
}
