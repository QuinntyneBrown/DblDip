using DblDip.Domain.Features.Meetings;

namespace DblDip.Testing.Builders
{
    public class MeetingDtoBuilder
    {
        private MeetingDto _meetingDto;

        public static MeetingDto WithDefaults()
        {
            return new MeetingDto();
        }

        public MeetingDtoBuilder()
        {
            _meetingDto = new MeetingDto();
        }

        public MeetingDto Build()
        {
            return _meetingDto;
        }
    }
}
