using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreatePhotoGallery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.PhotoGallery).NotNull();
                RuleFor(request => request.PhotoGallery).SetValidator(new PhotoGalleryValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public PhotoGalleryDto PhotoGallery { get; init; }
        }

        public class Response
        {
            public PhotoGalleryDto PhotoGallery { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photoGallery = new PhotoGallery(default);

                _context.Add(photoGallery);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PhotoGallery = photoGallery.ToDto()
                };
            }
        }
    }
}
