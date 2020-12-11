using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using static System.Guid;

namespace DblDip.Core.Models
{
    public class Feedback : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Feedback(Email email, string description)
        {
            Apply(new FeedbackCreated(NewGuid(), email, description));
        }
        public void When(FeedbackCreated feedbackCreated)
        {
            FeedbackId = feedbackCreated.FeedbackId;
            RespondentEmail = feedbackCreated.RespondentEmail;
            Description = feedbackCreated.Description;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid FeedbackId { get; private set; }
        public Email RespondentEmail { get; private set; }
        public string Description { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
