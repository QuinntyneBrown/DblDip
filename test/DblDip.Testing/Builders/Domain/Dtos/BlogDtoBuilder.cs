using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
