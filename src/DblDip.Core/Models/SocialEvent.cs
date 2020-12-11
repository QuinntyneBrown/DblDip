using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class SocialEvent : PhotographyProject
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }
        public override DateRange Scheduled { get; }
        public Guid SocialEventId { get; private set; }
    }
}
