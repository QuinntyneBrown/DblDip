namespace ShootQ.Core.DomainEvents
{
    public class RoleRemoved
    {
        public RoleRemoved(string value)
        {
            Name = value;
        }

        public string Name { get; }
    }
}
