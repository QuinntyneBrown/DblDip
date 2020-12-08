using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class PhotoGallery : AggregateRoot
    {
        public PhotoGallery(string name)
        {
            Apply(new PhotoGalleryCreated(Guid.NewGuid(), name));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(PhotoGalleryCreated photoGalleryCreated)
        {
            PhotoGalleryId = photoGalleryCreated.PhotoGalleryId;
            Name = photoGalleryCreated.Name;
            Photos = new HashSet<Photo>();
        }

        protected override void EnsureValidState()
        {

        }

        public Guid PhotoGalleryId { get; private set; }
        public string Name { get; private set; }
        public ICollection<PhotoGallery.Photo> Photos { get; private set; }

        public record Photo
        {
            public Guid DigitalAssetId { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
    }
}
}
