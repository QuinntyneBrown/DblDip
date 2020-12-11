using BuildingBlocks.Abstractions;
using System;

namespace DblDip.Core.Models
{
    public class Blog: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid BlogId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }

    public record PostReference(Guid PostId, string Title);
}
