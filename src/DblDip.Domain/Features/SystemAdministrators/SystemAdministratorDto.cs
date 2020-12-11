using System;

namespace DblDip.Domain.Features.SystemAdministrators
{
    public class SystemAdministratorDto
    {
        public Guid SystemAdministratorId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
