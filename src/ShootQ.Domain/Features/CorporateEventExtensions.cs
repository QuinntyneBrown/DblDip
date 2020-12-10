using ShootQ.Core.Models;
using ShootQ.Domain.Features.CorporateEvents;

namespace ShootQ.Domain.Features
{
    public static class CorporateEventExtensions
    {
        public static CorporateEventDto ToDto(this CorporateEvent corporateEvent)
        {
            return new CorporateEventDto(corporateEvent.CorporateEventId);
        }
    }
}
