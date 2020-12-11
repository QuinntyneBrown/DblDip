using ShootQ.Core.Models;
using ShootQ.Domain.Features.Questionnaires;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class QuestionnaireDtoBuilder
    {
        private QuestionnaireDto _questionnaireDto;

        public static QuestionnaireDto WithDefaults()
        {
            return new QuestionnaireDto();
        }

        public QuestionnaireDtoBuilder()
        {
            _questionnaireDto = WithDefaults();
        }

        public QuestionnaireDto Build()
        {
            return _questionnaireDto;
        }
    }
}
