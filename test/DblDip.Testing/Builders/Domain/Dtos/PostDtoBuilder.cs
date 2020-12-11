using DblDip.Core.Models;
using DblDip.Domain.Features.Posts;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class PostDtoBuilder
    {
        private PostDto _postDto;

        public static PostDto WithDefaults()
        {
            return new PostDto();
        }

        public PostDtoBuilder()
        {
            _postDto = WithDefaults();
        }

        public PostDto Build()
        {
            return _postDto;
        }
    }
}
