using ShootQ.Core.Models;
using ShootQ.Domain.Features.DigitalAssets;


namespace ShootQ.Domain.Features
{
    public static class DigitalAssetExtensions
    {
        public static DigitalAssetDto ToDto(this DigitalAsset digitalAsset)
            => new DigitalAssetDto(digitalAsset.DigitalAssetId, digitalAsset.Name, digitalAsset.Bytes, digitalAsset.ContentType);
    }
}
