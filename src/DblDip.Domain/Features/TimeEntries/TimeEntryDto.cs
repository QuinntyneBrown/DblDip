using System;

namespace DblDip.Domain.Features.TimeEntries
{
    public class TimeEntryDto
    {
        public Guid TimeEntryId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
