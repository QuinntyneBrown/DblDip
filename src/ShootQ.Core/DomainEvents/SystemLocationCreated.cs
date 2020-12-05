using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record SystemLocationCreated(Guid SystemLocationId, string Name, Location Location);
}
