using ShootQ.Domain.Features.Meetings;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
