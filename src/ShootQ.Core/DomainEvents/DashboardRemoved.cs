using System;

namespace ShootQ.Core.DomainEvents
{
    public class DashboardRemoved
    {
        public DashboardRemoved(DateTime deleted)
        {
            Deleted = deleted;
        }

        public DateTime Deleted { get; }
    }
}
