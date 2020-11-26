using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class DashboardBuilder
    {
        private Dashboard _dashboard;

        public DashboardBuilder()
        {
            _dashboard = new Dashboard();
        }

        public Dashboard Build()
        {
            return _dashboard;
        }
    }
}
