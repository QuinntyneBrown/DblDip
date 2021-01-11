using DblDip.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    [Owned]
    public class SurveyResult
    {
        public Guid SurveyResultId { get; set; }
        public Email RespondentEmail { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public SurveyResult(Guid surveyResultId, Email respondentEmail, IEnumerable<Answer> answers)
        {
            SurveyResultId = surveyResultId;
            RespondentEmail = respondentEmail;
            Answers = answers;
        }
        public SurveyResult()
        {

        }
    }
}
