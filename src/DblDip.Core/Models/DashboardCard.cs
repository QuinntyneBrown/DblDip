using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class DashboardCard
    {
        public Guid DashboardCardId { get; set; }

        public string Options { get; set; }
        public DashboardCard()
        {

        }

        public DashboardCard(Guid dashboardCardId, dynamic options)
        {
            DashboardCardId = dashboardCardId;
            Options = options;
        }
    }
}
