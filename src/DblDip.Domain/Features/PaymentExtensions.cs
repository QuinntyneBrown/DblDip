using DblDip.Core.Models;
using DblDip.Domain.Features.Payments;

namespace DblDip.Domain.Features
{
    public static class PaymentExtensions
    {
        public static PaymentDto ToDto(this Payment payment)
        {
            return new PaymentDto
            {

            };
        }
    }
}
