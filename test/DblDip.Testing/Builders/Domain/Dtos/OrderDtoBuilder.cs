using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
