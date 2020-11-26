using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Dashboard: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid DashboardId { get; private set; }

        public class DashboardCard
        {
            public Guid CardId { get; set; }
        }
    }
}
