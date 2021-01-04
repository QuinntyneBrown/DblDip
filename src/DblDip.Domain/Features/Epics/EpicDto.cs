using System;

namespace DblDip.Domain.Features
{
    public class EpicDto
    {
        public Guid EpicId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
