namespace ShootQ.Core.DomainEvents
{
    public class LeadCreate
    {
        public LeadCreate(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
