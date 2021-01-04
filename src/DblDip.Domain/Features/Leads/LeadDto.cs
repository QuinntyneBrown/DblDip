using System;

namespace DblDip.Domain.Features
{
    public class LeadDto
    {
        public Guid LeadId { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public string EmailAddress { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
