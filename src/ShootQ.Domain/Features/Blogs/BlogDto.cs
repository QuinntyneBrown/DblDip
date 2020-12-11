using System;

namespace ShootQ.Domain.Features.Blogs
{
    public class BlogDto
    {
        public Guid BlogId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
