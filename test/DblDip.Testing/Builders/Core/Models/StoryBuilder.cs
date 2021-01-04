using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class StoryBuilder
    {
        private Story _story;

        public static Story WithDefaults()
        {
            return new Story();
        }

        public StoryBuilder()
        {
            _story = WithDefaults();
        }

        public Story Build()
        {
            return _story;
        }
    }
}
