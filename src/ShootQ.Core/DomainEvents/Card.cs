using System;

namespace ShootQ.Core.DomainEvents
{
    public record CardCreated(Guid CardId, string Name, string Description);
}
