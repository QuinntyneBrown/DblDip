using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Questionnaire: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid QuestionnaireId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
