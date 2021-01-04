using System;

namespace DblDip.Domain.Features
{
    public record PostDto(Guid PostId, Guid AuthorId, string Title, string Body);
}
