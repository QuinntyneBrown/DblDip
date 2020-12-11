using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class PostBuilder
    {
        private Post _post;

        public static Post WithDefaults()
        {
            return new Post();
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
