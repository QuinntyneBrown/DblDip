using System;

namespace ShootQ.Domain.Features.EditedPhotos
{
    public class EditedPhotoDto
    {
        public Guid EditedPhotoId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
