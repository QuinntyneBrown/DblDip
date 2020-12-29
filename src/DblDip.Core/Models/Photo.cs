using System;

namespace DblDip.Core.Models
{
    public record Photo(Guid DigitalAssetId, string Name, DateTime Created);
}
