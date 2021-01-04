using DblDip.Domain.Features.Clients;

namespace DblDip.Testing.Builders
{
    public class ClientDtoBuilder
    {
        private ClientDto _clientDto;

        public static ClientDto WithDefaults()
        {
            return new(default, default, default, default, default);
        }

        public ClientDtoBuilder()
        {
            _clientDto = WithDefaults();
        }

        public ClientDto Build()
        {
            return _clientDto;
        }
    }
}
