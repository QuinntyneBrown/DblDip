using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.PhotoGalleries
{
    public class UpdatePhotoGallery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.PhotoGallery).NotNull();
                RuleFor(request => request.PhotoGallery).SetValidator(new PhotoGalleryValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public PhotoGalleryDto PhotoGallery { get; set; }
        }

        public class Response
        {
            public PhotoGalleryDto PhotoGallery { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var photoGallery = await _context.FindAsync<PhotoGallery>(request.PhotoGallery.PhotoGalleryId);

                //photoGallery.Update();

                _context.Store(photoGallery);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PhotoGallery = photoGallery.ToDto()
                };
            }
        }
    }
}
