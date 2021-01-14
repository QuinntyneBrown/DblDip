using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class YouTubeVideo : AggregateRoot
    {
        public Guid YouTubeVideoId { get; private set; }
        public string NativeYouTubeVideoId { get; private set; }
        public string Description { get; private set; }
        public DateTime? Deleted { get; private set; }
        protected YouTubeVideo()
        {

        }
        protected override void When(dynamic @event) => When(@event);

        public YouTubeVideo(string value)
        {
            Apply(new YouTubeVideoCreated(Guid.NewGuid()));
        }
        public void When(DomainEvents.YouTubeVideoCreated youTubeVideoCreated)
        {

        }

        public void When(YouTubeVideoRemoved youTubeVideoRemoved)
        {
            Deleted = youTubeVideoRemoved.Deleted;
        }

        public void When(YouTubeVideoUpdated youTubeVideoUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }


        public void Remove(DateTime deleted)
        {
            Apply(new YouTubeVideoRemoved(deleted));
        }

        public void Update()
        {

        }
    }
}
