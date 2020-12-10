using BuildingBlocks.Abstractions;
using ShootQ.Core.Enums;
using System;
using System.Collections.Generic;

namespace ShootQ.Core.Models
{
    public class Role : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid RoleId { get; private set; }
        public string Name { get; set; }
        public ICollection<Privilege> Privileges { get; private set; }

        public record Privilege(AccessRight AccessRight, string Aggregate);

    }

}
