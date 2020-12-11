using System;
using System.Collections.Generic;

namespace ShootQ.Core.DomainEvents
{
    public record AccountCreated (Guid AccountId, ICollection<Guid> ProfileIds, Guid DefaultProfileId, string Name, Guid AccountHolderUserId);
}
