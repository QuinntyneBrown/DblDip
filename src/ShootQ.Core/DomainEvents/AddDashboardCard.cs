namespace ShootQ.Core.DomainEvents
{
    public class AddDashboardCard
    {
        public AddDashboardCard(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
