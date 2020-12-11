using DblDip.Core.Models;
using DblDip.Domain.Features.Surveys;

namespace DblDip.Domain.Features
{
    public static class SurveyExtensions
    {
        public static SurveyDto ToDto(this Survey survey)
            => new SurveyDto(survey.SurveyId, survey.Name);
    }
}
