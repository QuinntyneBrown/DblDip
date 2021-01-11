using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Domain.Features
{
    public record ClientDto(Guid ClientId, string Firstname, string Lastname, Email Email);
}
