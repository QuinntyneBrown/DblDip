using DblDip.Core.Models;
using DblDip.Domain.Features.Libraries;

namespace DblDip.Testing.Builders.Domain.Dtos
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
