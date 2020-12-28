using System;

namespace DblDip.Domain.Features.Payments
{
    public class PaymentDto
    {
        public Guid PaymentId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
