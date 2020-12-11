using DblDip.Core.Models;
using System;
using System.Collections.Generic;

namespace DblDip.Domain.Features.PhotoGalleries
{
    public record PhotoGalleryDto(Guid PhotoGalleryId, Guid PhotographerId, ICollection<Photo> Photos);
}
