using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Role: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid RoleId { get; private set; }
    }
}
