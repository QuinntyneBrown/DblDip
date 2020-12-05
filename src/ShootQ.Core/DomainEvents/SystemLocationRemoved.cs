using System;

namespace ShootQ.Core.DomainEvents
{
    public class SystemLocationRemoved
    {
        public SystemLocationRemoved(DateTime deleted)
        {
            Deleted = deleted;
        }

        public DateTime Deleted { get; }
    }
}
