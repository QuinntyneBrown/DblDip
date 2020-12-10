using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class StudioPortrait : PhotographyJob
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid StudoPortraitId { get; private set; }

        public override DateRange Scheduled => throw new NotImplementedException();
    }
}
