using DblDip.Core.DomainEvents;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Core.Models
{
    public class ProjectManager : Profile
    {
        protected ProjectManager()
        {

        }

        public ProjectManager(string name, Email email)
            : base(name, email, typeof(ProjectManager))
        {
            Apply(new ProjectManagerCreated(base.ProfileId));
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

        public void Update()
        {
            Apply(new ProjectManagerUpdated());
        }

        public Guid ProjectManagerId { get; private set; }
    }
}
