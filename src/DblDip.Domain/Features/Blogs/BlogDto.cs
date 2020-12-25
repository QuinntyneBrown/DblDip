using System;

namespace DblDip.Domain.Features.Blogs
{
    public class BlogDto
    {
        public Guid BlogId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
