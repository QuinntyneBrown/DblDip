using System;

namespace ShootQ.Core.DomainEvents
{
    public class DashboardCreated
    {
        public DashboardCreated(string name, Guid dashboardId, Guid userId)
        {
            DashboardId = dashboardId;
            UserId = userId;
            Name = name;
        }

        public string Name { get; }
        public Guid UserId { get; set; }
        public Guid DashboardId { get; set; }
    }
}
