using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Survey: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Survey(string name)
        {
            Apply(new SurveyCreated(Guid.NewGuid(),name));
        }
        public void When(SurveyCreated surveyCreated)
        {
            SurveyId = surveyCreated.SurveyId;
            Name = surveyCreated.Name;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid SurveyId { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<SurveyQuestion>  SurveyQuestions { get; private set; }
    }

    public record SurveyQuestion(string Value);
}
