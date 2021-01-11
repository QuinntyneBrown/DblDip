using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record AvatarUpdated(Guid AvatarDigitalAssetId): Event;
}
