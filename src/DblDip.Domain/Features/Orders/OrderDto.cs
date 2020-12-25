using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features.Orders
{
    public class OrderDto
    {
        public Guid OrderId { get; init; }
        public decimal Total { get; init; }
        public ICollection<LineItemDto> LineItems { get; init; }

        public record LineItemDto
        {

        }
    }
}
