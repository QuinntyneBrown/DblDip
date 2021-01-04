using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class SocialEventBuilder
    {
        private SocialEvent _socialEvent;

        public static SocialEvent WithDefaults()
        {
            return new SocialEvent();
        }

        public SocialEventBuilder()
        {
            _socialEvent = WithDefaults();
        }

        public SocialEvent Build()
        {
            return _socialEvent;
        }
    }
}
