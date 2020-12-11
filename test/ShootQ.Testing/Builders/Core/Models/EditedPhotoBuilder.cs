using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class EditedPhotoBuilder
    {
        private EditedPhoto _editedPhoto;

        public static EditedPhoto WithDefaults()
        {
            return new EditedPhoto();
        }

        public EditedPhotoBuilder()
        {
            _editedPhoto = WithDefaults();
        }

        public EditedPhoto Build()
        {
            return _editedPhoto;
        }
    }
}
