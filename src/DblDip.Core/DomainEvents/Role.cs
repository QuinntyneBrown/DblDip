using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record PrivilegesUpdated(ICollection<Privilege> Privileges);
    public record RoleCreated(Guid RoleId, string Name);
    public record RoleRemoved(DateTime Removed);
}
