using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class Dashboard : AggregateRoot
    {
        public Dashboard(string name, Guid userId)
        {
            Apply(new DashboardCreated(Guid.NewGuid(), userId, name));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(DashboardCreated dashboardCreated)
        {
            Name = dashboardCreated.Name;
            DashboardId = dashboardCreated.DashboardId;
            UserId = dashboardCreated.UserId;
        }

        public void When(DashboardRemoved dashboardRemoved)
        {
            Deleted = dashboardRemoved.Deleted;
        }

        public void When(DashboardUpdated dashboardUpdated)
        {
            Name = dashboardUpdated.Name;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new DashboardRemoved(deleted));
        }

        public void Update(string value)
        {
            Apply(new DashboardUpdated(value));
        }

        public Guid DashboardId { get; private set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public record DashboardCard(Guid DashboardCardId, dynamic Options);
        public DateTime? Deleted { get; set; }
    }
}
