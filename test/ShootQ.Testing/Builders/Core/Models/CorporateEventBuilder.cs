using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class CorporateEventBuilder
    {
        private CorporateEvent _corporateEvent;

        public static CorporateEvent WithDefaults()
        {
            return new CorporateEvent();
        }

        public CorporateEventBuilder()
        {
            _corporateEvent = WithDefaults();
        }

        public CorporateEvent Build()
        {
            return _corporateEvent;
        }
    }
}
