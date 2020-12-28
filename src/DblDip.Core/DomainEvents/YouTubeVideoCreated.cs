using System;

namespace DblDip.Core.DomainEvents
{
    public record YouTubeVideoCreated(Guid YouTubeVideoId);
    public record YouTubeVideoRemoved(DateTime Deleted);
    public record YouTubeVideoUpdated();
}
