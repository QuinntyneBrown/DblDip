using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class PhotoGallery : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid PhotoGalleryId { get; private set; }

        public record Photo
        {
            public Guid DigitalAssetId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}
}
