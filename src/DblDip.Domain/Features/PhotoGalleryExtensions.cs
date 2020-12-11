using DblDip.Core.Models;
using DblDip.Domain.Features.PhotoGalleries;

namespace DblDip.Domain.Features
{
    public static class PhotoGalleryExtensions
    {
        public static PhotoGalleryDto ToDto(this PhotoGallery photoGallery)
        {
            return new PhotoGalleryDto(photoGallery.PhotoGalleryId, photoGallery.PhotographerId, photoGallery.Photos);
        }
    }
}
