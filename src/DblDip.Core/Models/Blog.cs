using BuildingBlocks.Abstractions;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class Blog : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid BlogId { get; private set; }
        public string Name { get; private set; }
        public Email AuthorEmail { get; set; }
        public DateTime? Deleted { get; private set; }
    }

    public record PostReference(Guid PostId, string Title);
}
