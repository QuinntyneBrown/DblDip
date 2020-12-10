using ShootQ.Domain.Features.PhotoGalleries;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class PhotoGalleryDtoBuilder
    {
        private PhotoGalleryDto _photoGalleryDto;

        public static PhotoGalleryDto WithDefaults()
        {
            return new PhotoGalleryDto(default, default, default);
        }

        public PhotoGalleryDtoBuilder()
        {
            _photoGalleryDto = WithDefaults();
        }

        public PhotoGalleryDto Build()
        {
            return _photoGalleryDto;
        }
    }
}
