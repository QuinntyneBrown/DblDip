using System;

namespace ShootQ.Core.DomainEvents
{
    public class UserCreated
    {
        public UserCreated(Guid userId, string username, string password, byte[] salt)
            => (UserId, Username, Password, Salt) = (userId, username, password, salt);

        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
