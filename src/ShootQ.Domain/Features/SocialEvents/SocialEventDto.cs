using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.SocialEvents
{
    public class SocialEventDto
    {
        public DateRange Scheduled { get; }
        public Guid SocialEventId { get; set; }
    }
}
