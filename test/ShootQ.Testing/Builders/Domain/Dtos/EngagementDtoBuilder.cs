using ShootQ.Core.Models;
using ShootQ.Domain.Features.Engagements;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class EngagementDtoBuilder
    {
        private EngagementDto _engagementDto;

        public static EngagementDto WithDefaults()
        {
            return new EngagementDto();
        }

        public EngagementDtoBuilder()
        {
            _engagementDto = WithDefaults();
        }

        public EngagementDto Build()
        {
            return _engagementDto;
        }
    }
}
