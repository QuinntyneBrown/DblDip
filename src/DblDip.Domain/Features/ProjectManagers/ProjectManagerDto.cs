using System;

namespace DblDip.Domain.Features.ProjectManagers
{
    public class ProjectManagerDto
    {
        public Guid ProjectManagerId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
