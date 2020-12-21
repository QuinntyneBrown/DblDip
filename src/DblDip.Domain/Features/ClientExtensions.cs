using DblDip.Core.Models;
using DblDip.Domain.Features.Clients;

namespace DblDip.Domain.Features
{
    public static class ClientExtensions
    {
        public static ClientDto ToDto(this Client client)
            => new(client.ClientId, client.Firstname, client.Lastname, client.Email, client.PhoneNumber);
    }
}
