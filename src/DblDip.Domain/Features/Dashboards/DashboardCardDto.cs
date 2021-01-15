using System;

namespace DblDip.Domain.Features
{
    public record DashboardCardDto(Guid DashboardCardId, Guid DashboardId, DashboardCardOptionsDto Options);
}
