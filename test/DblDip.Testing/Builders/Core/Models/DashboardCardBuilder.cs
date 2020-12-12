using DblDip.Core.Models;
using static DblDip.Core.Models.Dashboard;

namespace DblDip.Testing.Builders.Core.Models
{
    public class DashboardCardBuilder
    {
        private DashboardCard _dashboardCard;

        public static DashboardCard WithDefaults()
        {
            return new DashboardCard(default, default);
        }

        public DashboardCardBuilder()
        {
            _dashboardCard = WithDefaults();
        }

        public DashboardCard Build()
        {
            return _dashboardCard;
        }
    }
}
