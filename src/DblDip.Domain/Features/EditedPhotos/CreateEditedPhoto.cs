using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateEditedPhoto
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.EditedPhoto).NotNull();
                RuleFor(request => request.EditedPhoto).SetValidator(new EditedPhotoValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public EditedPhotoDto EditedPhoto { get; init; }
        }

        public class Response: ResponseBase
        {
            public EditedPhotoDto EditedPhoto { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var editedPhoto = new EditedPhoto();

                _store.Add(editedPhoto);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    EditedPhoto = editedPhoto.ToDto()
                };
            }
        }
    }
}
