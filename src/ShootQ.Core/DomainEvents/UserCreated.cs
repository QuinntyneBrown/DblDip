using System;

namespace ShootQ.Core.DomainEvents
{
    public class UserCreated
    {
        public UserCreated(string username, string password, byte[] salt)
        {
            Username = username;
            Password = password;
            Salt = salt;
        }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
