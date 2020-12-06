namespace ShootQ.Core.DomainEvents
{
    public class DashboardUpdated
    {
        public DashboardUpdated(string value)
        {
            Name = value;
        }

        public string Name { get; }
    }
}
