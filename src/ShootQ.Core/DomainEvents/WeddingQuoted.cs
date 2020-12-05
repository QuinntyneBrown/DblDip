namespace ShootQ.Core.DomainEvents
{
    public class WeddingQuoted
    {
        public WeddingQuoted(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
