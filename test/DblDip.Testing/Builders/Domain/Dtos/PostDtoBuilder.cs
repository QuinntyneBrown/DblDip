using DblDip.Domain.Features.Posts;

namespace DblDip.Testing.Builders
{
    public class PostDtoBuilder
    {
        private PostDto _postDto;

        public static PostDto WithDefaults()
            => new(default, default, default, default);

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
