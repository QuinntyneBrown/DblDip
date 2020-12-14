using BuildingBlocks.Abstractions;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
{
    public class ShotList: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid ShotListId { get; private set; }
        public string PhotographyProject { get; set; }
        public ICollection<Shot> Shots { get; set; }
        public DateTime? Deleted { get; private set; }

    }

    public record Shot(string Name, string Description);
}
