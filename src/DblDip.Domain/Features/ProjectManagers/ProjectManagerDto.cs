using System;

namespace DblDip.Domain.Features.ProjectManagers
{
    public class ProjectManagerDto
    {
        public Guid ProjectManagerId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
