using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record PrivilegesUpdated(ICollection<Privilege> Privileges): Event;
    public record RoleCreated(Guid RoleId, string Name): Event;
    public record RoleRemoved(DateTime Removed): Event;
}
