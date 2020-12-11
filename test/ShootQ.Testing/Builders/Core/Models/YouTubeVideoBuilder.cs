using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class YouTubeVideoBuilder
    {
        private YouTubeVideo _youTubeVideo;

        public static YouTubeVideo WithDefaults()
        {
            return new YouTubeVideo(default);
        }

        public YouTubeVideoBuilder()
        {
            _youTubeVideo = WithDefaults();
        }

        public YouTubeVideo Build()
        {
            return _youTubeVideo;
        }
    }
}
