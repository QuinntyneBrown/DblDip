using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class EditedPhoto : Service
    {
        public EditedPhoto()
            : base(default, default, default)
        {

        }

        protected override void When(dynamic @event) => When(@event);

        public void When(EditedPhotoUpdated editedPhotoUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Update()
        {

        }

        public Guid EditedPhotoId { get; private set; }
    }
}
