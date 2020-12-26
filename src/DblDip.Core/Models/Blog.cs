using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Blog : AggregateRoot
    {
        public Blog()
        {
            Apply(new BlogCreated(Guid.NewGuid()));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(BlogCreated blogCreated)
        {
            BlogId = blogCreated.BlogId;
        }

        public void When(BlogPostAdded blogPostAdded)
        {
            _posts.Add(new(blogPostAdded.PostId, blogPostAdded.Title));
        }

        public void When(BlogRemoved blogRemoved)
        {
            Deleted = blogRemoved.Deleted;
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

        public Guid BlogId { get; private set; }
        public string Name { get; private set; }
        public Email AuthorEmail { get; set; }
        public DateTime? Deleted { get; private set; }
        public IReadOnlyList<PostReference> Posts => _posts;

        private readonly List<PostReference> _posts;
    }
}
