using System;

namespace DblDip.Domain.Features
{
    public class DashboardCardDto
    {
        public Guid DashboardCardId { get; init; }
        public Guid DashboardId { get; init; }
        public DashboardCardOptionsDto Options { get; init; }
    }
}
