namespace ShootQ.Core.DomainEvents
{
    public class UserPasswordChanged
    {
        public UserPasswordChanged(string password)
        {
            Password = password;
        }

        public string Password { get; }
    }
}
