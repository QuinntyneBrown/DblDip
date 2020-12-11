using ShootQ.Core.DomainEvents;
using ShootQ.Core.Interfaces;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public abstract class PhotographyProject : Service, IScheduledAggregate
    {
        public PhotographyProject()
            :base(default, default, default)
        {

        }

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
