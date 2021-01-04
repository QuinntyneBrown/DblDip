using DblDip.Core.Models;
using DblDip.Core.ValueObjects;

namespace DblDip.Testing.Builders
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
