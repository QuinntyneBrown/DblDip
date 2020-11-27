namespace ShootQ.Core.DomainEvents
{
    public class UserRoleRemoved
    {
        public UserRoleRemoved(string value)
        {
            Name = value;
        }

        public string Name { get; }
    }
}
