using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class ClientBuilder
    {
        private Client _client;

        public static Client WithDefaults()
        {
            return new Client("Test", (Email)"test@test.com");
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
