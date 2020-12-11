using System;

namespace ShootQ.Domain.Features.YouTubeVideos
{
    public class YouTubeVideoDto
    {
        public Guid YouTubeVideoId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
