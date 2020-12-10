using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class ContactBuilder
    {
        private Contact _contact;

        public static Contact WithDefaults()
        {
            return new Contact();
        }

        public ContactBuilder()
        {
            _contact = WithDefaults();
        }

        public Contact Build()
        {
            return _contact;
        }
    }
}
