using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.ProjectManagers
{
    public class UpdateProjectManager
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProjectManager).NotNull();
                RuleFor(request => request.ProjectManager).SetValidator(new ProjectManagerValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public ProjectManagerDto ProjectManager { get; set; }
        }

        public class Response
        {
            public ProjectManagerDto ProjectManager { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var projectManager = await _context.FindAsync<ProjectManager>(request.ProjectManager.ProjectManagerId);

                projectManager.Update(default);

                _context.Store(projectManager);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    ProjectManager = projectManager.ToDto()
                };
            }
        }
    }
}
