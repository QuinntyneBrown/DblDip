using BuildingBlocks.EventStore;
namespace DblDip.Core.DomainEvents
{
    public record PhotoGallerySent(string Value): Event;
}
