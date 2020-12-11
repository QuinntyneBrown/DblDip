using DblDip.Core.Models;
using DblDip.Domain.Features.Orders;

namespace DblDip.Domain.Features
{
    public static class OrderExtensions
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {

            };
        }
    }
}
