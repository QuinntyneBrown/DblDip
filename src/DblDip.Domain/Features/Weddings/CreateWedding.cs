using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using DblDip.Core.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace DblDip.Domain.Features
{
    public class CreateWedding
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public record Request(int Hours, DateTime DateTime, double Longitude, double Latitude) : IRequest<Response>;
        
        public record Response(WeddingDto Wedding);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;
            private readonly IConfiguration _configuration;

            public Handler(IDblDipDbContext context, IConfiguration configuration)
            {
                _context = context;
                _configuration = configuration;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var longitude = Convert.ToDouble(_configuration["DefaultLocation:Longitude"]);

                var latitude = Convert.ToDouble(_configuration["DefaultLocation:Latitude"]);

                var home = Location.Create(longitude, latitude).Value;

                var location = Location.Create(request.Longitude, request.Latitude).Value;

                var wedding = new Wedding(home, home, location, request.DateTime, request.Hours);

                _context.Add(wedding);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response(wedding.ToDto());
            }
        }
    }
}
