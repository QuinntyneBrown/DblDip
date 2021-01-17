using BuildingBlocks.EventStore;
using DblDip.Core;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateDashboard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Dashboard).NotNull();
                RuleFor(request => request.Dashboard).SetValidator(new DashboardValidator());
            }
        }

        public record Request(DashboardDto Dashboard) : IRequest<Response>;

        public record Response(DashboardDto Dashboard);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IEventStore store, IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                _store = store;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var profileId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(Constants.ClaimTypes.ProfileId).Value);

                var dashboard = new Dashboard(request.Dashboard.Name, profileId);

                _store.Add(dashboard);

                await _store.SaveChangesAsync(cancellationToken);

                return new(dashboard.ToDto());
            }
        }
    }
}
