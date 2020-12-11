using DblDip.Core.Models;
using System;

namespace DblDip.Testing.Builders.Core.Models
{
    public class DashboardBuilder
    {
        private Dashboard _dashboard;

        public static Dashboard WithDefaults(Guid userId)
        {
            return new Dashboard("Default", userId);
        }

        public DashboardBuilder()
        {
            _dashboard = new Dashboard("Default", Guid.NewGuid());
        }

        public Dashboard Build()
        {
            return _dashboard;
        }
    }
}
