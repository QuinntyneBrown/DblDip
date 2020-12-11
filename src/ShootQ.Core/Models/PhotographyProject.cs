using BuildingBlocks.Abstractions;
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

        public Guid PhotographerId { get; set; }

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
        public DateTime? GallerySent { get; private set; }
    }
}
