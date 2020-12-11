using System;
using System.Collections.Generic;
using static DblDip.Core.Models.Dashboard;

namespace DblDip.Core.DomainEvents
{
    public record DashboardCreated(Guid DashboardId, Guid UserId, string Name);
    public record DashboardCardAdded(Guid DashboardCardId, Guid CardId, dynamic Options);
    public record DashboardCardRemoved(Guid DashboardCardId);
    public record DashboardCardUpdated(Guid DashboardCardId, dynamic Options);
    public record DashboardRemoved(DateTime Deleted);
    public record DashboardCardsUpdated(IEnumerable<DashboardCard> DashboardCards);
    public record DashboardUpdated(string Name);
}
