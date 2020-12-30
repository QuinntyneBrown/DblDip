using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Questionnaire : AggregateRoot
    {
        public Questionnaire()
        {
            Apply(new QuestionnaireCreated(Guid.NewGuid()));
        }
        protected override void When(dynamic @event) => When(@event);

        public void When(QuestionnaireCreated questionnaireCreated)
        {
            QuestionnaireId = questionnaireCreated.QuestionnaireId;
        }

        public void When(QuestionnaireUpdated questionnaireUpdated)
        {

        }

        public void When(QuestionnaireRemoved questionnaireRemoved)
        {
            Deleted = questionnaireRemoved.Deleted;
        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {
        }

        public void Remove(DateTime deleted)
        {
            Apply(new QuestionnaireRemoved(deleted));
        }

        public Guid QuestionnaireId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
