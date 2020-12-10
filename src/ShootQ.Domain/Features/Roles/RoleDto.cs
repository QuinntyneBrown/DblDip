using System;
using System.Collections.Generic;
using static ShootQ.Core.Models.Role;

namespace ShootQ.Domain.Features.Roles
{
    public record RoleDto(Guid RoleId, string Name, ICollection<Privilege> Privileges);
}
