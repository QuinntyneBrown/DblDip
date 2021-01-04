using System;

namespace DblDip.Domain.Features
{
    public record DigitalAssetDto(Guid DigitalAssetId, string Name, byte[] Bytes, string ContentType);
}
