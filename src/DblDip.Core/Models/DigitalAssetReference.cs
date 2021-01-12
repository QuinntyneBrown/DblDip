using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{

    [Owned]
    public class DigitalAssetReference
    {
        public Guid DigitalAssetId { get; set; }
        public DigitalAssetReference()
        {

        }

        public DigitalAssetReference(Guid digitalAssetId)
        {
            DigitalAssetId = digitalAssetId;
        }
    }
}
