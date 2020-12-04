using ShootQ.Core.Models;
using System;
using System.Collections.Generic;

namespace ShootQ.Domain.Features.Dashboards
{
    public class DashboardDto
    {
        public Guid DashboardId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Dashboard.DashboardCard> DashboardCards { get; set; }
        = new HashSet<Dashboard.DashboardCard>();
    }
}
