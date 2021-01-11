using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class SurveyQuestion
    {
        public Guid QuestionId { get; set; }
        public string Value { get; set; }
        public SurveyQuestion(Guid questionId, string value)
        {
            QuestionId = questionId;
            Value = value;
        }

        public SurveyQuestion()
        {

        }
    };
}
