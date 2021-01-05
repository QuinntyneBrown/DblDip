using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class DashboardExtensions
    {
        public static DashboardDto ToDto(this Dashboard dashboard)
        {
            return new DashboardDto
            {
                DashboardId = dashboard.DashboardId,
                Name = dashboard.Name,
                ProfileId = dashboard.ProfileId
            };
        }
    }
}
