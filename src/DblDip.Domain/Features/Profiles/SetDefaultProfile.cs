using FluentValidation;
using MediatR;
using BuildingBlocks.Abstractions;
using DblDip.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace DblDip.Domain.Features.Profiles
{
    public class SetDefaultProfile
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {  

        }

        public class Response
        {

        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) {            
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                return new Response()
                {

                };
            }
        }
    }
}
