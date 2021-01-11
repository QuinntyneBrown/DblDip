using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DblDip.Core.Models
{
    [Owned]
    public class DashboardCard
    {
        public Guid DashboardCardId { get; set; }
        [NotMapped]
        public dynamic Options { get; set; }
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
