using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace DblDip.Core.Models
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

        public void When(DashboardCardsUpdated dashboardCardsUpdated)
        {
            DashboardCards = dashboardCardsUpdated.DashboardCards;
        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(DateTime deleted)
        {
            Apply(new DashboardRemoved(deleted));
        }

        public void Update(string name)
        {
            Apply(new DashboardUpdated(name));
        }

        public void UpdateDashboardCards(IEnumerable<DashboardCard> dashboardCards)
        {
            Apply(new DashboardCardsUpdated(dashboardCards));
        }

        public Guid DashboardId { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<DashboardCard> DashboardCards { get; private set; }
        public bool IsDefault { get; private set; }

        public record DashboardCard(Guid DashboardCardId, dynamic Options);
        public DateTime? Deleted { get; private set; }
    }
}
