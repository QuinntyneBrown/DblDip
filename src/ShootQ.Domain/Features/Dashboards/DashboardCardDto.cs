using System;

namespace ShootQ.Domain.Features.Dashboards
{
    public class DashboardCardDto
    {
        public Guid DashboardCardId { get; set; }
        public Guid DashboardId { get; set; }
        public dynamic Options { get; set; }
    }
}
