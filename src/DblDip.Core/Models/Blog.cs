using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Blog : AggregateRoot
    {
        private List<PostReference> _posts;
        public Guid BlogId { get; private set; }
        public string Name { get; private set; }
        public Email AuthorEmail { get; private set; }
        public DateTime? Deleted { get; private set; }
        public List<PostReference> Posts => _posts.ToList();

        public Blog()
        {
            Apply(new BlogCreated(Guid.NewGuid()));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(BlogCreated blogCreated)
        {
            BlogId = blogCreated.BlogId;
            _posts = new();
        }

        public void When(BlogPostAdded blogPostAdded)
        {
            _posts.Add(new(blogPostAdded.PostId, blogPostAdded.Title));
        }

        public void When(BlogRemoved blogRemoved)
        {
            Deleted = blogRemoved.Deleted;
        }

        public void When(BlogUpdated blogUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void AddPost(Guid postId, string title)
        {
            Apply(new BlogPostAdded(postId, title));
        }

        public void Remove(DateTime deleted)
        {
            Apply(new BlogRemoved(deleted));
        }

        public void Update()
        {
            Apply(new BlogUpdated());
        }
    }
}
