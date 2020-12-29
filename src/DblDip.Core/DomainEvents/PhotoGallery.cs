using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotoGalleryCreated(Guid PhotoGalleryId, string Name);
    public record PhotoGalleryUpdated;
    public record PhotoGalleryRemoved (DateTime Deleted);
}
