using System.Collections.Generic;

namespace DblDip.Domain.Features
{
    public class BlogPageDto
    {
        public IEnumerable<PostDto> Posts { get; init; }
        public int TotalResults { get; init; }
        public int TotalPages { get; init; }
        public int CurrentPage { get; init; }
    }
}
