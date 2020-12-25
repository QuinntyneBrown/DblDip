using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features.Dashboards
{
    public class DashboardDto
    {
        public Guid DashboardId { get; init; }
        public Guid UserId { get; init; }
        public string Name { get; init; }

        public ICollection<Dashboard.DashboardCard> DashboardCards { get; init; }
        = new HashSet<Dashboard.DashboardCard>();
    }
}
