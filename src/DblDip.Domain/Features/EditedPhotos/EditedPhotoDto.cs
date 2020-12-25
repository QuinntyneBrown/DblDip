using System;

namespace DblDip.Domain.Features.EditedPhotos
{
    public class EditedPhotoDto
    {
        public Guid EditedPhotoId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
