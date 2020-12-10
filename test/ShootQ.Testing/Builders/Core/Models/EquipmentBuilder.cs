using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class EquipmentBuilder
    {
        private Equipment _equipment;

        public static Equipment WithDefaults()
        {
            return new Equipment();
        }

        public EquipmentBuilder()
        {
            _equipment = WithDefaults();
        }

        public Equipment Build()
        {
            return _equipment;
        }
    }
}
