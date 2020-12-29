using System;

namespace DblDip.Domain.Features.Discounts
{
    public class DiscountDto
    {
        public Guid DiscountId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
