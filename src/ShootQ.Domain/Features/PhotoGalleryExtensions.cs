using ShootQ.Core.Models;
using ShootQ.Domain.Features.PhotoGalleries;

namespace ShootQ.Domain.Features
{
    public static class PhotoGalleryExtensions
    {
        public static PhotoGalleryDto ToDto(this PhotoGallery photoGallery)
        {
            return new PhotoGalleryDto(photoGallery.PhotoGalleryId, photoGallery.PhotographerId, photoGallery.Photos);
        }
    }
}
