using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Post: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid PostId { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Body { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
