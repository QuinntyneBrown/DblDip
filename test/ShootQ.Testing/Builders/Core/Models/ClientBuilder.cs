using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class ClientBuilder
    {
        private Client _client;

        public static Client WithDefaults()
        {
            return new Client();
        }

        public ClientBuilder()
        {
            _client = WithDefaults();
        }

        public Client Build()
        {
            return _client;
        }
    }
}
