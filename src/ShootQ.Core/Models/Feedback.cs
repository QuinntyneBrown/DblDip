using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Feedback: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Feedback(Email email, string description)
        {
            Apply(new FeedbackCreated(Guid.NewGuid(),email,description));
        }
        public void When(FeedbackCreated feedbackCreated)
        {
            FeedbackId = feedbackCreated.FeedbackId;
            ClientEmail = feedbackCreated.ClientEmail;
            Description = feedbackCreated.Description;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid FeedbackId { get; private set; }
        public Email ClientEmail { get; private set; }
        public string Description { get; private set; }
    }
}
