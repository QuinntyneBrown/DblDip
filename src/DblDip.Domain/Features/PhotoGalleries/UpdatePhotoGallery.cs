using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
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
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var photoGallery = await _store.FindAsync<PhotoGallery>(request.PhotoGallery.PhotoGalleryId);

                photoGallery.Update();

                _store.Add(photoGallery);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PhotoGallery = photoGallery.ToDto()
                };
            }
        }
    }
}
