using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class ConsultationBuilder
    {
        private Consultation _consultation;

        public static Consultation WithDefaults()
        {
            return new Consultation(default, default, default);
        }

        public ConsultationBuilder()
        {
            _consultation = WithDefaults();
        }

        public Consultation Build()
        {
            return _consultation;
        }
    }
}
