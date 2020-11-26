using System;

namespace ShootQ.Core.DomainEvents
{
    public class LeadCreated
    {
        public LeadCreated()
        {
            LeadId = Guid.NewGuid();
        }

        public Guid LeadId { get; }
    }
}
