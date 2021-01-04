using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetPhotoGalleryById
    {
        public class Request : IRequest<Response>
        {
            public Guid PhotoGalleryId { get; init; }
        }

        public class Response
        {
            public PhotoGalleryDto PhotoGallery { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photoGallery = await _context.FindAsync<PhotoGallery>(request.PhotoGalleryId);

                return new Response()
                {
                    PhotoGallery = photoGallery.ToDto()
                };
            }
        }
    }
}
