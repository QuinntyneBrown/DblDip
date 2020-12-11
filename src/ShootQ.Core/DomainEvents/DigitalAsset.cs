using System;

namespace ShootQ.Core.DomainEvents
{
    public record DigitalAssetCreated(Guid DigitalAssetId, string Name, byte[] Bytes, string ContentType);
}
