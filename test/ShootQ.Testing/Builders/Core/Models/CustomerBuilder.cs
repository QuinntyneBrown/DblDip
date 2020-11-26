using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class CustomerBuilder
    {
        private Customer _customer;

        public CustomerBuilder()
        {
            _customer = new Customer();
        }

        public Customer Build()
        {
            return _customer;
        }
    }
}
