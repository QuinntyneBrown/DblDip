using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class SocialEvent : PhotographyProject
    {
        public override DateRange Scheduled { get; }
        public Guid SocialEventId { get; private set; }
        public SocialEvent()
        {

        }

        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }
    }
}
