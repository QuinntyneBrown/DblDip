using System;

namespace DblDip.Domain.Features.Posts
{
    public class PostDto
    {
        public Guid PostId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
