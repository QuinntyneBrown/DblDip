namespace ShootQ.Core.DomainEvents
{
    public class UserRoleAdded
    {
        public UserRoleAdded(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
