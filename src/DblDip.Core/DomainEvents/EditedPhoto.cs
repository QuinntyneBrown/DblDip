using BuildingBlocks.EventStore;
namespace DblDip.Core.DomainEvents
{
    public record EditedPhotoUpdated (string Value): Event;
}
