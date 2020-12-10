using System;
using System.Collections.Generic;
using static ShootQ.Core.Models.Role;

namespace ShootQ.Core.DomainEvents
{
    public record PrivilegesUpdated(ICollection<Privilege> Privileges);
    public record RoleCreated (Guid RoleId, string Name);
    public record RoleRemoved(DateTime Removed);
}
