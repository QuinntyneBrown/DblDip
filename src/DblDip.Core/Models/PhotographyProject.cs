using DblDip.Core.DomainEvents;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public abstract class PhotographyProject : Service, IScheduledAggregate
    {
        public void When(PhotoGallerySent sent)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void SendGallery()
        {
            Apply(new PhotoGallerySent(default));
        }

        public abstract DateRange Scheduled { get; }
        public Guid PhotographerId { get; private set; }
        public Email ParticipantEmail { get; private set; }
        public PhoneNumber ParticipantPhoneNumber { get; private set; }
        public DateTime? GallerySent { get; private set; }
    }
}
