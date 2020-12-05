using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using ShootQ.Core.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace ShootQ.Domain.Features.Weddings
{
    public class CreateWedding
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Response> {
            public Guid CustomerId { get; set; }
            public int Hours { get; set; }
            public Guid PhotographyRateId { get; set; }
            public DateTime DateTime { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class Response
        {
            public WeddingDto Wedding { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IConfiguration _configuration;

            public Handler(IAppDbContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var longitude = Convert.ToDouble(_configuration["DefaultLocation:Longitude"]);
                
                var latitude = Convert.ToDouble(_configuration["DefaultLocation:Latitude"]);

                var home = Location.Create(longitude,latitude).Value;

                var location = Location.Create(request.Longitude, request.Latitude).Value;

                var wedding = new Wedding(home, home, location, request.DateTime, request.Hours, request.PhotographyRateId);

                _context.Store(wedding);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Wedding = wedding.ToDto()
                };
            }
        }
    }
}
