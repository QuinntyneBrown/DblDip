using DblDip.Core.ValueObjects;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class ClientDtoBuilder
    {
        private ClientDto _clientDto;

        public static ClientDto WithDefaults()
        {
            return new(default, "Quinntyne", "Brown", (Email)"quinntyne@hotmail.com");
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
