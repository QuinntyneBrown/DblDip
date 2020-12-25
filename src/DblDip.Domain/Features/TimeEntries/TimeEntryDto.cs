using System;

namespace DblDip.Domain.Features.TimeEntries
{
    public class TimeEntryDto
    {
        public Guid TimeEntryId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
