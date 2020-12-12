using System;

namespace DblDip.Domain.Features.Epics
{
    public class EpicDto
    {
        public Guid EpicId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
