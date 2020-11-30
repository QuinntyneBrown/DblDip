using System;

namespace ShootQ.Core.DomainEvents
{
    public class PhotoGalleryCreated
    {
        public PhotoGalleryCreated(Guid photoGalleryId, string name)
        {
            PhotoGalleryId = photoGalleryId;
            Name = name;
        }

        public Guid PhotoGalleryId { get; set; }
        public string Name { get; }
    }
}
