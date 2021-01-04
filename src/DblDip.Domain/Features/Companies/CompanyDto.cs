using System;

namespace DblDip.Domain.Features
{
    public class CompanyDto
    {
        public Guid CompanyId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
