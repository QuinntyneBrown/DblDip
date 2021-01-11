using DblDip.Core.Data;
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

        public class Response
        {
            public EditedPhotoDto EditedPhoto { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var editedPhoto = new EditedPhoto();

                _context.Add(editedPhoto);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    EditedPhoto = editedPhoto.ToDto()
                };
            }
        }
    }
}
