using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Core.DomainEvents
{
    public record DashboardCreated(Guid DashboardId, Guid ProfileId, string Name): Event;
    public record DashboardCardAdded(Guid DashboardCardId, Guid CardId, dynamic Options): Event;
    public record DashboardCardRemoved(Guid DashboardCardId): Event;
    public record DashboardCardUpdated(Guid DashboardCardId, dynamic Options): Event;
    public record DashboardRemoved(DateTime Deleted): Event;
    public record DashboardCardsUpdated(ICollection<DashboardCard> DashboardCards): Event;
    public record DashboardUpdated(string Name): Event;
    public record DashboardDefault(bool IsDefault): Event;
}
