using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class MeetingBuilder
    {
        private Meeting _meeting;

        public static Meeting WithDefaults()
        {
            return new Meeting();
        }

        public MeetingBuilder()
        {
            _meeting = WithDefaults();
        }

        public Meeting Build()
        {
            return _meeting;
        }
    }
}
