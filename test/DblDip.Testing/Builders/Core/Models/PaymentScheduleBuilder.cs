using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class PaymentScheduleBuilder
    {
        private PaymentSchedule _paymentSchedule;

        public static PaymentSchedule WithDefaults()
        {
            return new PaymentSchedule();
        }

        public PaymentScheduleBuilder()
        {
            _paymentSchedule = WithDefaults();
        }

        public PaymentSchedule Build()
        {
            return _paymentSchedule;
        }
    }
}
