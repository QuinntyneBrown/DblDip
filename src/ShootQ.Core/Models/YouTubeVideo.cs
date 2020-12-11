using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class YouTubeVideo: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public YouTubeVideo(string value)
        {
            Apply(new DomainEvents.YouTubeVideo(value));
        }
        public void When(DomainEvents.YouTubeVideo youTubeVideoCreated)
        {

        }

        public void When(YouTubeVideoRemoved youTubeVideoRemoved)
        {

        }

        public void When(YouTubeVideoUpdated youTubeVideoUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }


        public void Remove(string value)
        {
            Apply(new YouTubeVideoRemoved(value));
        }

        public void Update(string value)
        {
            Apply(new YouTubeVideoUpdated(value));
        }

        public Guid YouTubeVideoId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}