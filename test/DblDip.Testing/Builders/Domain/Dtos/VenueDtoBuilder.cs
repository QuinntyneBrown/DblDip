using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
