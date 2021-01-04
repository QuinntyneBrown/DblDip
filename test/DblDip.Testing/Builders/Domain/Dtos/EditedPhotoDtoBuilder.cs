using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
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
