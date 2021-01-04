using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class LibraryBuilder
    {
        private Library _library;

        public static Library WithDefaults()
        {
            return new Library();
        }

        public LibraryBuilder()
        {
            _library = WithDefaults();
        }

        public Library Build()
        {
            return _library;
        }
    }
}
