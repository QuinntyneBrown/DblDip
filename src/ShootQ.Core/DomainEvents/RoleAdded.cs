namespace ShootQ.Core.DomainEvents
{
    public class RoleAdded
    {
        public RoleAdded(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
