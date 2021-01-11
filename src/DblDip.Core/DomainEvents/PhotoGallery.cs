using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record PhotoGalleryCreated(Guid PhotoGalleryId, string Name): Event;
    public record PhotoGalleryUpdated: Event;
    public record PhotoGalleryRemoved (DateTime Deleted): Event;
}
