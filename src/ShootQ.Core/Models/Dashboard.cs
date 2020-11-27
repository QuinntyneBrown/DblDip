using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class Dashboard : AggregateRoot
    {
        public Dashboard(string name, Guid userId)
        {
            Apply(new DashboardCreated(name, Guid.NewGuid(), userId));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(DashboardCreated dashboardCreated)
        {
            Name = dashboardCreated.Name;
            DashboardId = dashboardCreated.DashboardId;
            UserId = dashboardCreated.UserId;
        }

        protected override void EnsureValidState()
        {

        }

        public Guid DashboardId { get; private set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public record DashboardCard
        {
            public Guid CardId { get; set; }
        public dynamic Options { get; set; }
    }
}
}
