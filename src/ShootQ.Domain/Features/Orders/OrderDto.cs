using System;
using System.Collections.Generic;

namespace ShootQ.Domain.Features.Orders
{
    public class OrderDto
    {
        public Guid OrderId { get; private set; }
        public decimal Total { get; set; }
        public ICollection<LineItemDto> LineItems { get; set; }
        
        public record LineItemDto
        {

        }
    }
}
