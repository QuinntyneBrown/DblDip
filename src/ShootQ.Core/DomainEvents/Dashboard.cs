using System;

namespace ShootQ.Core.DomainEvents
{
    public record DashboardCreated(string Name, Guid UserId, Guid DashboardId);
    public record DashboardCardAdded(Guid DashboardCardId, Guid CardId, dynamic Options);
    public record DashboardCardRemoved(Guid DashboardCardId);
    public record DashboardCardUpdated(Guid DashboardCardId, dynamic Options);
    public record DashboardRemoved(DateTime Deleted);
}
