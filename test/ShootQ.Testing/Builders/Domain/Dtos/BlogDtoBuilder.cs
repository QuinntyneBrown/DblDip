using ShootQ.Core.Models;
using ShootQ.Domain.Features.Blogs;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class BlogDtoBuilder
    {
        private BlogDto _blogDto;

        public static BlogDto WithDefaults()
        {
            return new BlogDto();
        }

        public BlogDtoBuilder()
        {
            _blogDto = WithDefaults();
        }

        public BlogDto Build()
        {
            return _blogDto;
        }
    }
}
