namespace ShootQ.Core.DomainEvents
{
    public class Remove
    {
        public Remove(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
