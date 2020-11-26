using System;
using System.Security.Cryptography;

namespace ShootQ.Core.DomainEvents
{
    public class UserCreated
    {
        public UserCreated(string username, string password)
        {
            Username = username;
            Password = password;

            Salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Salt);
            }
        }

        public byte[] Salt { get; private set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
