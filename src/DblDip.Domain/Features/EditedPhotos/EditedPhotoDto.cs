using System;

namespace DblDip.Domain.Features
{
    public class EditedPhotoDto
    {
        public Guid EditedPhotoId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
