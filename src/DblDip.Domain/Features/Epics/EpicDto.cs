using System;

namespace DblDip.Domain.Features.Epics
{
    public class EpicDto
    {
        public Guid EpicId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
