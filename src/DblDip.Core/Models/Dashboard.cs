using BuildingBlocks.EventStore;
using DblDip.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DblDip.Core.Models
{
    public class Dashboard : AggregateRoot
    {
        protected Dashboard()
        {

        }

        private ICollection<DashboardCard> _dashboardCards;
        public Guid DashboardId { get; private set; }
        public Guid ProfileId { get; private set; }
        public string Name { get; private set; }
        public bool IsDefault { get; private set; }
        public DateTime? Deleted { get; private set; }
        public IReadOnlyList<DashboardCard> DashboardCards => _dashboardCards.ToList();

        public Dashboard(IEnumerable<IEvent> events)
            : base(events) { }

        public Dashboard(string name, Guid profileId)
        {
            Apply(new DashboardCreated(Guid.NewGuid(), profileId, name));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(DashboardCreated dashboardCreated)
        {
            Name = dashboardCreated.Name;
            DashboardId = dashboardCreated.DashboardId;
            ProfileId = dashboardCreated.ProfileId;
        }

        public void When(DashboardRemoved dashboardRemoved)
        {
            Deleted = dashboardRemoved.Deleted;
        }

        public void When(DashboardUpdated dashboardUpdated)
        {
            Name = dashboardUpdated.Name;
        }

        public void When(DashboardDefault dashboardDefault)
        {
            IsDefault = dashboardDefault.IsDefault;
        }
        public void When(DashboardCardsUpdated dashboardCardsUpdated)
        {
            _dashboardCards = dashboardCardsUpdated.DashboardCards;
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

        public void SetDefault(bool isDefault = true)
        {
            Apply(new DashboardDefault(isDefault));
        }

        public void UpdateDashboardCards(ICollection<DashboardCard> dashboardCards)
        {
            Apply(new DashboardCardsUpdated(dashboardCards));
        }
    }
}
