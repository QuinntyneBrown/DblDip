using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class Library: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid LibraryId { get; private set; }
        public Guid PhotographerId { get; set; }
        public ICollection<Guid> MyImages { get; private set; }
        public ICollection<Guid> MyFiles { get; set; }
        public DateTime? Deleted { get; private set; }
    }
}
