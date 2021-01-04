using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
