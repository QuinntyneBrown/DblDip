using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class FamilyPortrait: PhotographyProject 
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid FamilyPortraitId { get; private set; }
        public DateTime? Deleted { get; private set; }

        public override DateRange Scheduled { get; }
    }
}
