using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class PaymentBuilder
    {
        private Payment _payment;

        public static Payment WithDefaults()
        {
            return new Payment();
        }

        public PaymentBuilder()
        {
            _payment = WithDefaults();
        }

        public Payment Build()
        {
            return _payment;
        }
    }
}
