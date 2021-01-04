using System;

namespace DblDip.Domain.Features
{
    public class BlogDto
    {
        public Guid BlogId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
