using ShootQ.Core.Models;
using ShootQ.Domain.Features.Dashboards;

namespace ShootQ.Domain.Features
{
    public static class DashboardExtensions
    {
        public static DashboardDto ToDto(this Dashboard dashboard)
        {
            return new DashboardDto
            {
                DashboardId = dashboard.DashboardId,
                Name = dashboard.Name,
                UserId = dashboard.UserId
            };
        }
    }
}
