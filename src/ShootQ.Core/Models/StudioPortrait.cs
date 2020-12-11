using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class StudioPortrait : PhotographyProject
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid StudioPortraitId { get; private set; }

        public override DateRange Scheduled => throw new NotImplementedException();
    }
}
