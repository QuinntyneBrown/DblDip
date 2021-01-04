using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
