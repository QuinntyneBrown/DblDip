using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features.Clients
{
    public record ClientDto(Guid ClientId, string Firstname, string Lastname, Email Email, string PhoneNumber);
}
