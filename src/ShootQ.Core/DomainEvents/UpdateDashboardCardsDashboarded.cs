namespace ShootQ.Core.DomainEvents
{
    public class UpdateDashboardCardsDashboarded
    {
        public UpdateDashboardCardsDashboarded(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
