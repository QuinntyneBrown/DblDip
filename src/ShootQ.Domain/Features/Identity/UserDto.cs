using System;

namespace ShootQ.Domain.Features.Identity
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
