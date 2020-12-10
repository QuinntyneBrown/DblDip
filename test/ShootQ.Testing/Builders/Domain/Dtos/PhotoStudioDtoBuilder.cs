using ShootQ.Domain.Features.PhotoStudios;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class PhotoStudioDtoBuilder
    {
        private PhotoStudioDto _photoStudioDto;

        public static PhotoStudioDto WithDefaults()
        {
            return new PhotoStudioDto();
        }

        public PhotoStudioDtoBuilder()
        {
            _photoStudioDto = new PhotoStudioDto();
        }

        public PhotoStudioDto Build()
        {
            return _photoStudioDto;
        }
    }
}
