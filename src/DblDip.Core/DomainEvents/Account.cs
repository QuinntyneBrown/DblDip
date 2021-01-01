using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record AccountCreated(Guid AccountId, ICollection<Guid> ProfileIds, Guid DefaultProfileId, string Name, Guid AccountHolderUserId);
    public record AccountRemoved(DateTime Deleted);
    public record AccountUpdated(string Value);
    public record SetDefaultProfile(Guid ProfileId);
    public record SetCurrentProfile(Guid ProfileId);
}
