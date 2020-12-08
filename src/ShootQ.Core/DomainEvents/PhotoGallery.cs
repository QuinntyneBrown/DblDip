using System;

namespace ShootQ.Core.DomainEvents
{
    public record PhotoGalleryCreated(Guid PhotoGalleryId, string Name);
}
