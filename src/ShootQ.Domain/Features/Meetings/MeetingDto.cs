using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Meetings
{
    public class MeetingDto
    {
        public Guid MeetingId { get; set; }
        public DateRange Scheduled { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
