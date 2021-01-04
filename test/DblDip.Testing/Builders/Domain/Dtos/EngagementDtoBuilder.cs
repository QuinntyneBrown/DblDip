using DblDip.Core.Models;
using DblDip.Domain.Features.Engagements;

namespace DblDip.Testing.Builders
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
