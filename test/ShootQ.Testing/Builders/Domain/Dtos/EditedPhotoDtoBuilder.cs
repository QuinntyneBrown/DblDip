using ShootQ.Core.Models;
using ShootQ.Domain.Features.EditedPhotos;

namespace ShootQ.Testing.Builders.Domain.Dtos
{
    public class EditedPhotoDtoBuilder
    {
        private EditedPhotoDto _editedPhotoDto;

        public static EditedPhotoDto WithDefaults()
        {
            return new EditedPhotoDto();
        }

        public EditedPhotoDtoBuilder()
        {
            _editedPhotoDto = WithDefaults();
        }

        public EditedPhotoDto Build()
        {
            return _editedPhotoDto;
        }
    }
}
