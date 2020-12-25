using System;

namespace DblDip.Domain.Features.Dashboards
{
    public class DashboardCardDto
    {
        public Guid DashboardCardId { get; init; }
        public Guid DashboardId { get; init; }
        public dynamic Options { get; init; }
    }
}
