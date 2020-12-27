using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features.Roles
{
    public record RoleDto(Guid RoleId, string Name, ICollection<Privilege> Privileges);
}
