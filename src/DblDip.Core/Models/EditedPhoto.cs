using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class EditedPhoto: Service
    {
        public EditedPhoto()
            : base(default, default, default)
        {

        }

        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid EditedPhotoId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
