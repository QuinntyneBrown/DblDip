using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Clients
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Email Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
