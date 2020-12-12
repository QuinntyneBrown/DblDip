using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class ReceiptBuilder
    {
        private Receipt _receipt;

        public static Receipt WithDefaults()
        {
            return new Receipt();
        }

        public ReceiptBuilder()
        {
            _receipt = WithDefaults();
        }

        public Receipt Build()
        {
            return _receipt;
        }
    }
}
