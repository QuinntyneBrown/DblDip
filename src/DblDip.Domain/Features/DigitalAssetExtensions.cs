using DblDip.Core.Models;
using DblDip.Domain.Features.DigitalAssets;


namespace DblDip.Domain.Features
{
    public static class DigitalAssetExtensions
    {
        public static DigitalAssetDto ToDto(this DigitalAsset digitalAsset)
            => new DigitalAssetDto(digitalAsset.DigitalAssetId, digitalAsset.Name, digitalAsset.Bytes, digitalAsset.ContentType);
    }
}
