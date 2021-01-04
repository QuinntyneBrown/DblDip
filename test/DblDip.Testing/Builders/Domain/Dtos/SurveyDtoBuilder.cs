using DblDip.Domain.Features.Surveys;

namespace DblDip.Testing.Builders
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
