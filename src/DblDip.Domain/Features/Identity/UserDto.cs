using System;

namespace DblDip.Domain.Features.Identity
{
    public class UserDto
    {
        public Guid UserId { get; init; }
        public string Username { get; init; }
    }
}
