using DblDip.Core.Models;
using DblDip.Domain.Features.Profiles;

namespace DblDip.Testing.Builders
{
    public class ProfileDtoBuilder
    {
        private ProfileDto _profileDto;

        public static ProfileDto WithDefaults()
        {
            return new ProfileDto();
        }

        public ProfileDtoBuilder()
        {
            _profileDto = WithDefaults();
        }

        public ProfileDto Build()
        {
            return _profileDto;
        }
    }
}
