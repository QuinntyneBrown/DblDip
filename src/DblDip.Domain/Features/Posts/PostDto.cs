using System;

namespace DblDip.Domain.Features.Posts
{
    public record PostDto(Guid PostId, Guid AuthorId, string Title);
}
