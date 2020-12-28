using DblDip.Core.Models;
using DblDip.Domain.Features.Posts;

namespace DblDip.Domain.Features
{
    public static class PostExtensions
    {
        public static PostDto ToDto(this Post post)
            => new(post.PostId, post.AuthorId, post.Title, post.Body);
    }
}
