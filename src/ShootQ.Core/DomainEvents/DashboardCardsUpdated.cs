using System.Collections.Generic;
using static ShootQ.Core.Models.Dashboard;

namespace ShootQ.Core.DomainEvents
{
    public record DashboardCardsUpdated(IEnumerable<DashboardCard> DashboardCards);

}
