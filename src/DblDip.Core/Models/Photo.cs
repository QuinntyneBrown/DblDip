using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class Photo
    {
        public Guid DigitalAssetId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public Photo()
        {

        }

        public Photo(Guid digitalAssetId, string name, DateTime created)
        {
            DigitalAssetId = digitalAssetId;
            Name = name;
            Created = created;
        }
    }
}
