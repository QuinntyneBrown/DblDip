using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotoGalleryCreated(Guid PhotoGalleryId, string Name);
}
