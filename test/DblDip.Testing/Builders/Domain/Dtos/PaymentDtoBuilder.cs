
using DblDip.Core.Models;
using DblDip.Domain.Features.Payments;

namespace DblDip.Testing.Builders.Domain.Dtos
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
