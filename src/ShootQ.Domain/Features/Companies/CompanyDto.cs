using System;

namespace ShootQ.Domain.Features.Companies
{
    public class CompanyDto
    {
        public Guid CompanyId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
