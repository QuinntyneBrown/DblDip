using System;

namespace ShootQ.Core.DomainEvents
{
    public class DashboardCardRemoved
    {
        public DashboardCardRemoved(Guid dashboardCardId)
        {
            DashboardCardId = dashboardCardId;
        }

        public Guid DashboardCardId { get; }
    }
}
