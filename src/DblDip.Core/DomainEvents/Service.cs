using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record ServiceUpdated(Guid ServiceId, string Name, Guid DigitalAssetId, string Description): Event;
    public record ServiceCreated(Guid ServiceId, string Name, Guid DigitalAssetId, string Description): Event;
    public record ServiceRemoved(DateTime Deleted): Event;
}
