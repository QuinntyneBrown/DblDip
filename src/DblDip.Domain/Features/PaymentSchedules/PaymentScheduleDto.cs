using System;

namespace DblDip.Domain.Features
{
    public class PaymentScheduleDto
    {
        public Guid PaymentScheduleId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
