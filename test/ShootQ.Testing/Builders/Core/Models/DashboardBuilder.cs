using ShootQ.Core.Models;
using System;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class DashboardBuilder
    {
        private Dashboard _dashboard;

        public DashboardBuilder(string name, Guid userId)
        {
            _dashboard = new Dashboard(name, userId);
        }

        public Dashboard Build()
        {
            return _dashboard;
        }
    }
}
