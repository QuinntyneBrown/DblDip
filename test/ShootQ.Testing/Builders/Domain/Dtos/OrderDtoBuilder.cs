using ShootQ.Domain.Features.Orders;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class OrderDtoBuilder
    {
        private OrderDto _orderDto;

        public static OrderDto WithDefaults()
        {
            return new OrderDto();
        }

        public OrderDtoBuilder()
        {
            _orderDto = new OrderDto();
        }

        public OrderDto Build()
        {
            return _orderDto;
        }
    }
}
