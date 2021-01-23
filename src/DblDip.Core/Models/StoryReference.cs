using Microsoft.EntityFrameworkCore;
using System;

namespace DblDip.Core.Models
{
    [Owned]
    public class StoryReference
    {
        public Guid StoryId { get; init; }
        protected StoryReference()
        {

        }
        public StoryReference(Guid storyId)
        {
            StoryId = storyId;
        }
    }
}
