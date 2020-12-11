using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class QuestionnaireBuilder
    {
        private Questionnaire _questionnaire;

        public static Questionnaire WithDefaults()
        {
            return new Questionnaire();
        }

        public QuestionnaireBuilder()
        {
            _questionnaire = WithDefaults();
        }

        public Questionnaire Build()
        {
            return _questionnaire;
        }
    }
}
