using DblDip.Core.Models;
using DblDip.Domain.Features.Blogs;

namespace DblDip.Testing.Builders.Domain.Dtos
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
