using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record DigitalAssetCreated(Guid DigitalAssetId, string Name, byte[] Bytes, string ContentType): Event;
}
