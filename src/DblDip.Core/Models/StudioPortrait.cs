using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class StudioPortrait : PhotographyProject
    {
        public StudioPortrait()
        {

        }

        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid StudioPortraitId { get; private set; }

        public override DateRange Scheduled => throw new NotImplementedException();
    }
}
