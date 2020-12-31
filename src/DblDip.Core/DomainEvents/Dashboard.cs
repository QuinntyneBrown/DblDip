using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record DashboardCreated(Guid DashboardId, Guid ProfileId, string Name);
    public record DashboardCardAdded(Guid DashboardCardId, Guid CardId, dynamic Options);
    public record DashboardCardRemoved(Guid DashboardCardId);
    public record DashboardCardUpdated(Guid DashboardCardId, dynamic Options);
    public record DashboardRemoved(DateTime Deleted);
    public record DashboardCardsUpdated(ICollection<DashboardCard> DashboardCards);
    public record DashboardUpdated(string Name);
    public record DashboardDefault(bool IsDefault);
}
