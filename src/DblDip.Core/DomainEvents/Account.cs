using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record AccountCreated(Guid AccountId, ICollection<ProfileReference> Profiles, Guid DefaultProfileId, string Name, Guid AccountHolderUserId);
    public record AccountRemoved(DateTime Deleted);
    public record AccountUpdated(string Value);
}
