using System;

namespace ShootQ.Core.DomainEvents
{
    public record ServiceUpdated(Guid ServiceId, string Name, Guid DigitalAssetId, string Description);
    public record ServiceCreated(Guid ServiceId, string Name, Guid DigitalAssetId, string Description);
    public record ServiceRemoved(DateTime Deleted);
}
