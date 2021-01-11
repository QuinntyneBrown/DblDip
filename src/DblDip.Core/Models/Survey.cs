using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Survey : AggregateRoot
    {
        protected Survey()
        {

        }

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

        public void When(SurveyUpdated surveyUpdated)
        {

        }

        public void When(SurveyRemoved surveyRemoved)
        {
            Deleted = surveyRemoved.Deleted;
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

        public void Update()
        {
            Apply(new SurveyUpdated());
        }

        public void Remove(DateTime deleted)
        {
            Apply(new SurveyRemoved(deleted));
        }

        public Guid SurveyId { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<SurveyQuestion> SurveyQuestions { get; private set; }
        public IEnumerable<SurveyResult> SurveyResults { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
