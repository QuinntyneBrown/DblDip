using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class ParticipantBuilder
    {
        private Participant _participant;

        public static Participant WithDefaults()
        {
            return new Participant();
        }

        public ParticipantBuilder()
        {
            _participant = WithDefaults();
        }

        public Participant Build()
        {
            return _participant;
        }
    }
}
