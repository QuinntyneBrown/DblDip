using System;
using System.Collections.Generic;
using static DblDip.Core.Models.Role;

namespace DblDip.Domain.Features.Roles
{
    public record RoleDto(Guid RoleId, string Name, ICollection<Privilege> Privileges);
}
