using System;
using System.Collections.Generic;
using static DblDip.Core.Models.Role;

namespace DblDip.Core.DomainEvents
{
    public record PrivilegesUpdated(ICollection<Privilege> Privileges);
    public record RoleCreated(Guid RoleId, string Name);
    public record RoleRemoved(DateTime Removed);
}
