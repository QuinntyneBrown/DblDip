using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;

namespace DblDip.Core.Models
{
    public class Post : AggregateRoot
    {
        public Guid PostId { get; private set; }
        public Guid AuthorId { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Abstract { get; set; }
        public string Body { get; private set; }
        public string Categories { get; set; }
        public DateTime? Deleted { get; private set; }
        public DateTime? Published { get; private set; }

        protected Post()
        {

        }

        protected override void When(dynamic @event) => When(@event);

        public Post(Guid authorId, string title)
        {
            Apply(new PostCreated(Guid.NewGuid(), authorId, title, title.GenerateSlug()));
        }
        public void When(PostRemoved postRemoved)
        {
            Deleted = postRemoved.Deleted;
        }

        public void When(PostPublished postPublished)
        {
            Published = postPublished.Published;
        }

        public void When(PostCreated postCreated)
        {
            PostId = postCreated.PostId;
            AuthorId = postCreated.AuthorId;
            Title = postCreated.Title;
            Slug = postCreated.Slug;
        }

        public void When(PostBodyUpdated postBodyUpdated)
        {
            Body = postBodyUpdated.Body;
        }

        public void When(PostTitleUpdated postTitleUpdated)
        {
            Title = postTitleUpdated.Title;
            Slug = postTitleUpdated.Slug;
        }

        protected override void EnsureValidState()
        {
            if(string.IsNullOrEmpty(this.Title))
            {
                throw new Exception();
            }
        }

        public void Remove(DateTime deleted)
        {
            Apply(new PostRemoved(deleted));
        }

        public void Publish(DateTime published)
        {
            Apply(new PostPublished(published));
        }

        public void UpdateBody(string body)
        {
            Apply(new PostBodyUpdated(body));
        }

        public void UpdateTitle(string title)
        {
            Apply(new PostTitleUpdated(title, title.GenerateSlug()));
        }
    }
}
