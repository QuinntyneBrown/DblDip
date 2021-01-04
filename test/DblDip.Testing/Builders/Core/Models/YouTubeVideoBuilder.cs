using DblDip.Core.Models;

namespace DblDip.Testing.Builders
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
