using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.Interfaces;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public abstract class PhotographyProject : AggregateRoot, IScheduledAggregate
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
        public Guid ParticipantId { get; set; }
        public Guid AdditionalParticipantIds { get; private set; }
        public Guid? VendorId { get; private set; }
        public DateTime? GallerySent { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
