using ShootQ.Core.Models;
using System;
using System.Collections.Generic;

namespace ShootQ.Domain.Features.PhotoGalleries
{
    public record PhotoGalleryDto(Guid PhotoGalleryId, Guid PhotographerId, ICollection<Photo> Photos);
}
