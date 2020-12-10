using System;

namespace ShootQ.Domain.Features.DigitalAssets
{
    public record DigitalAssetDto(Guid DigitalAssetId, string Name, byte[] Bytes, string ContentType);
}
