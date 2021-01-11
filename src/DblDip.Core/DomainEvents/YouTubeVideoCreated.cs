using BuildingBlocks.EventStore;
using System;

namespace DblDip.Core.DomainEvents
{
    public record YouTubeVideoCreated(Guid YouTubeVideoId): Event;
    public record YouTubeVideoRemoved(DateTime Deleted): Event;
    public record YouTubeVideoUpdated(): Event;
}
