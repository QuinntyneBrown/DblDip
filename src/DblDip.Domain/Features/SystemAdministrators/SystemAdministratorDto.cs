using System;

namespace DblDip.Domain.Features.SystemAdministrators
{
    public class SystemAdministratorDto
    {
        public Guid SystemAdministratorId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
