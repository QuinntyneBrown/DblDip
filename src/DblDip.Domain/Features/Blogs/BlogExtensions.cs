using DblDip.Core.Models;

namespace DblDip.Domain.Features
{
    public static class BlogExtensions
    {
        public static BlogDto ToDto(this Blog blog)
        {
            return new BlogDto
            {
                BlogId = blog.BlogId
            };
        }
    }
}
