
using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class PaymentDtoBuilder
    {
        private PaymentDto _paymentDto;

        public static PaymentDto WithDefaults()
        {
            return new PaymentDto();
        }

        public PaymentDtoBuilder()
        {
            _paymentDto = WithDefaults();
        }

        public PaymentDto Build()
        {
            return _paymentDto;
        }
    }
}
