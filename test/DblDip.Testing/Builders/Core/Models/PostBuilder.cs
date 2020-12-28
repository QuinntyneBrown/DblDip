using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class PostBuilder
    {
        private Post _post;

        public static Post WithDefaults()
        {
            return new Post(default, default);
        }

        public PostBuilder()
        {
            _post = WithDefaults();
        }

        public Post Build()
        {
            return _post;
        }
    }
}
