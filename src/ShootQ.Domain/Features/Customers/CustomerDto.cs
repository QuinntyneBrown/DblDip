using System;

namespace ShootQ.Domain.Features.Customers
{
    public class CustomerDto
    {
        public Guid CustomerId { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
