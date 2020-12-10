using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShootQ.Core.Models
{
    public class Survey : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Survey(string name)
        {
            Apply(new SurveyCreated(Guid.NewGuid(), name));
        }
        public void When(SurveyCreated surveyCreated)
        {
            SurveyId = surveyCreated.SurveyId;
            Name = surveyCreated.Name;
            SurveyQuestions = new List<SurveyQuestion>();
            SurveyResults = new List<SurveyResult>();
        }

        public void When(SurveyQuestionAdded surveyAdded)
        {
            SurveyQuestions = SurveyQuestions.Concat(new[]
            {
                new SurveyQuestion(Guid.NewGuid(), surveyAdded.Value)
            });
        }

        public void When(SurveyResultAdded surveyResultAdded)
        {
            SurveyResults = SurveyResults.Concat(new[]
            {
                new SurveyResult(surveyResultAdded.SurveyResultId, surveyResultAdded.ClientEmail, surveyResultAdded.Answers)
            });
        }

        protected override void EnsureValidState()
        {

        }

        public void AddQuestion(string value)
        {
            Apply(new SurveyQuestionAdded(value));
        }

        public void AddSurveyResult(Email respondentEmail, IEnumerable<Answer> answers)
        {
            Apply(new SurveyResultAdded(Guid.NewGuid(), respondentEmail, answers));
        }

        public Guid SurveyId { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<SurveyQuestion> SurveyQuestions { get; private set; }
        public IEnumerable<SurveyResult> SurveyResults { get; private set; }
    }

    public record SurveyQuestion(Guid QuestionId, string Value);
    public record Answer(Guid QuestionId, int Value);
    public record SurveyResult(Guid SurveyResultId, Email RespondentEmail, IEnumerable<Answer> Answers);
}
