using ShootQ.Core.Models;
using ShootQ.Domain.Features.Customers;

namespace ShootQ.Domain.Features
{
    public static class CustomerExtensions
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.CustomerId
            };
        }
    }
}
