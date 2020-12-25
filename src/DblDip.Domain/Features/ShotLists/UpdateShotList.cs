using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.ShotLists
{
    public class UpdateShotList
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShotList).NotNull();
                RuleFor(request => request.ShotList).SetValidator(new ShotListValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ShotListDto ShotList { get; init; }
        }

        public class Response
        {
            public ShotListDto ShotList { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var shotList = await _context.FindAsync<ShotList>(request.ShotList.ShotListId);

                //shotList.Update();

                _context.Store(shotList);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    ShotList = shotList.ToDto()
                };
            }
        }
    }
}
