using ShootQ.Core.Models;
using ShootQ.Domain.Features.Posts;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
