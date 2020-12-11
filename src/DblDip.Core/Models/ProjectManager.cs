using BuildingBlocks.Abstractions;
using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class ProjectManager: Profile
    {
        public ProjectManager(string name, Email email)
            : base(new ProfileCreated(Guid.NewGuid(), name, email, nameof(Client), typeof(Client).AssemblyQualifiedName))
        {
            Apply(new ProjectManagerCreated(default));
        }

        protected override void When(dynamic @event) => When(@event);

        public void When(ProjectManagerCreated projectManagerCreated)
        {

        }

        public void When(ProjectManagerUpdated projectManagerUpdated)
        {

        }

        protected override void EnsureValidState()
        {

        }

        public void Update(string value)
        {
            Apply(new ProjectManagerUpdated(value));
        }

        public Guid ProjectManagerId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
