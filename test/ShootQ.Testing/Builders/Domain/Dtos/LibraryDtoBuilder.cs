using ShootQ.Core.Models;
using ShootQ.Domain.Features.Libraries;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class LibraryDtoBuilder
    {
        private LibraryDto _libraryDto;

        public static LibraryDto WithDefaults()
        {
            return new LibraryDto();
        }

        public LibraryDtoBuilder()
        {
            _libraryDto = WithDefaults();
        }

        public LibraryDto Build()
        {
            return _libraryDto;
        }
    }
}
