using System;

namespace DblDip.Domain.Features
{
    public class PaymentDto
    {
        public Guid PaymentId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
