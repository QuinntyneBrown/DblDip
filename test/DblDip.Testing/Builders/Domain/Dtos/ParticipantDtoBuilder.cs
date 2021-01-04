using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class ParticipantDtoBuilder
    {
        private ParticipantDto _participantDto;

        public static ParticipantDto WithDefaults()
        {
            return new ParticipantDto();
        }

        public ParticipantDtoBuilder()
        {
            _participantDto = WithDefaults();
        }

        public ParticipantDto Build()
        {
            return _participantDto;
        }
    }
}
