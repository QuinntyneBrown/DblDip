using System;

namespace ShootQ.Domain.Features.Leads
{
    public class LeadDto
    {
        public Guid LeadId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
