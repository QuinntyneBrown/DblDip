using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features
{
    public class SocialEventDto
    {
        public DateRange Scheduled { get; }
        public Guid SocialEventId { get; init; }
    }
}
