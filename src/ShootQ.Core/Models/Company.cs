using BuildingBlocks.Abstractions;
using ShootQ.Core.DomainEvents;
using System;

namespace ShootQ.Core.Models
{
    public class Company: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        public Company(string value)
        {
            Apply(new CompanyCreated(value));
        }
        public void When(CompanyCreated companyCreated)
        {

        }

        public void When(CompanyRemoved companyRemoved)
        {

        }

        public void When(CompanyUpdated companyUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Remove(string value)
        {
            Apply(new CompanyRemoved(value));
        }

        public void Update(string value)
        {
            Apply(new CompanyUpdated(value));
        }

        public Guid CompanyId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
