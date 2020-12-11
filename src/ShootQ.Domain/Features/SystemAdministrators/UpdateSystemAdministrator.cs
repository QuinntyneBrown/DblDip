using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.SystemAdministrators
{
    public class UpdateSystemAdministrator
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.SystemAdministrator).NotNull();
                RuleFor(request => request.SystemAdministrator).SetValidator(new SystemAdministratorValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public SystemAdministratorDto SystemAdministrator { get; set; }
        }

        public class Response
        {
            public SystemAdministratorDto SystemAdministrator { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var systemAdministrator = await _context.FindAsync<SystemAdministrator>(request.SystemAdministrator.SystemAdministratorId);

                systemAdministrator.Update(default);

                _context.Store(systemAdministrator);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    SystemAdministrator = systemAdministrator.ToDto()
                };
            }
        }
    }
}
