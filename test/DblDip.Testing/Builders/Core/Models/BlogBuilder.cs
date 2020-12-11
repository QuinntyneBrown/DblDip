using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class BlogBuilder
    {
        private Blog _blog;

        public static Blog WithDefaults()
        {
            return new Blog();
        }

        public BlogBuilder()
        {
            _blog = WithDefaults();
        }

        public Blog Build()
        {
            return _blog;
        }
    }
}
