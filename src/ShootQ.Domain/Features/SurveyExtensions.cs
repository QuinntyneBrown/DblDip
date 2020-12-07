using ShootQ.Core.Models;
using ShootQ.Domain.Features.Surveys;

namespace ShootQ.Domain.Features
{
    public static class SurveyExtensions
    {
        public static SurveyDto ToDto(this Survey survey)
            => new SurveyDto(survey.SurveyId, survey.Name);
    }
}
