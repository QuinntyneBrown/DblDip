using DblDip.Domain.Features.Clients;

namespace DblDip.Testing.Builders.Domain.Dtos
{
    public class ClientDtoBuilder
    {
        private ClientDto _clientDto;

        public static ClientDto WithDefaults()
        {
            return new ClientDto();
        }

        public ClientDtoBuilder()
        {
            _clientDto = new ClientDto();
        }

        public ClientDto Build()
        {
            return _clientDto;
        }
    }
}
