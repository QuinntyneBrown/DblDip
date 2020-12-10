using ShootQ.Domain.Features.Surveys;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class SurveyDtoBuilder
    {
        private SurveyDto _surveyDto;

        public static SurveyDto WithDefaults()
        {
            return new SurveyDto(default, "Test");
        }

        public SurveyDtoBuilder()
        {
            _surveyDto = WithDefaults();
        }

        public SurveyDto Build()
        {
            return _surveyDto;
        }
    }
}
