using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.SocialEvents
{
    public class SocialEventDto
    {
        public DateRange Scheduled { get; }
        public Guid SocialEventId { get; set; }
    }
}
