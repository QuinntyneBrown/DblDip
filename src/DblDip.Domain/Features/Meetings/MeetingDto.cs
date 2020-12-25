using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Meetings
{
    public class MeetingDto
    {
        public Guid MeetingId { get; init; }
        public DateRange Scheduled { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
