using ShootQ.Domain.Features.Customers;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class CustomerDtoBuilder
    {
        private CustomerDto _customerDto;

        public CustomerDtoBuilder()
        {
            _customerDto = new CustomerDto();
        }

        public CustomerDto Build()
        {
            return _customerDto;
        }
    }
}
