using System;

namespace ShootQ.Domain.Features.Posts
{
    public class PostDto
    {
        public Guid PostId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
