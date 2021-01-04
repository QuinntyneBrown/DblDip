using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class DiscountBuilder
    {
        private Discount _discount;

        public static Discount WithDefaults()
        {
            return new Discount();
        }

        public DiscountBuilder()
        {
            _discount = WithDefaults();
        }

        public Discount Build()
        {
            return _discount;
        }
    }
}
