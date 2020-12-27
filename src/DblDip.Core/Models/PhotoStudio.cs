using BuildingBlocks.Abstractions;
using System;
using DblDip.Core.ValueObjects;

namespace DblDip.Core.Models
{
    public class PhotoStudio : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }
        public string Name { get; private set; }
        public Location Location { get; private set; }
        public Guid PhotoStudioId { get; private set; }
    }
}
