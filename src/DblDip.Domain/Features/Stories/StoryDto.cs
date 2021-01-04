using System;

namespace DblDip.Domain.Features
{
    public class StoryDto
    {
        public Guid StoryId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
