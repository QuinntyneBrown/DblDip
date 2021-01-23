using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class FamilyPortrait : PhotographyProject
    {
        public Guid FamilyPortraitId { get; private set; }
        public override DateRange Scheduled { get; }
        public FamilyPortrait()
            :base()
        {

        }

        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }
    }
}
