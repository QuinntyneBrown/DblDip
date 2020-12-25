using System;

namespace DblDip.Domain.Features.YouTubeVideos
{
    public class YouTubeVideoDto
    {
        public Guid YouTubeVideoId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
