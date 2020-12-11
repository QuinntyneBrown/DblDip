using ShootQ.Core.Models;
using ShootQ.Domain.Features.Venues;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class VenueDtoBuilder
    {
        private VenueDto _venueDto;

        public static VenueDto WithDefaults()
        {
            return new VenueDto();
        }

        public VenueDtoBuilder()
        {
            _venueDto = WithDefaults();
        }

        public VenueDto Build()
        {
            return _venueDto;
        }
    }
}
