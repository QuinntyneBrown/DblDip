using DblDip.Core.Models;

namespace DblDip.Testing.Builders.Core.Models
{
    public class PhotoGalleryBuilder
    {
        private PhotoGallery _photoGallery;

        public static PhotoGallery WithDefaults()
        {
            return new PhotoGallery(default);
        }

        public PhotoGalleryBuilder()
        {
            _photoGallery = WithDefaults();
        }

        public PhotoGallery Build()
        {
            return _photoGallery;
        }
    }
}
