using System;

namespace DblDip.Domain.Features
{
    public class ProjectManagerDto
    {
        public Guid ProjectManagerId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
