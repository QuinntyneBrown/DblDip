using BuildingBlocks.Abstractions;
using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using ShootQ.Api;
using ShootQ.Core.Models;
using ShootQ.Testing.AuthenticationHandlers;
using ShootQ.Testing.Factories;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ShootQ.Testing
{
    public class ApiTestFixture : WebApplicationFactory<Startup>, IDisposable
    {
        private IAppDbContext _context;
        private IConfiguration _configuration;
        public ApiTestFixture()
        {
            checkpoint = new Checkpoint()
            {
                DbAdapter = DbAdapter.SqlServer,
                TablesToIgnore = new[]
                {
                    "__EFMigrationsHistory"
                }
            };

            _configuration = ConfigurationFactory.Create();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {

                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                }
            });
        }
        public IAppDbContext Context { get {

                if (_context == null)
                {
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"])
                        .Options;

                    var context = new EventStoreDbContext(options);
                    var dateTime = new MachineDateTime();
                    var eventStore = new EventStore(context, dateTime);
                    var aggregateSet = new AggregateSet(context, dateTime);

                    _context = new AppDbContext(eventStore, aggregateSet);
                }

                return _context;
            }
            set
            {
                _context = value;
            }
        }

        private Checkpoint checkpoint;

        public HttpClient CreateAuthenticatedClient(string token = null, string scheme = "Test")
        {
            if (string.IsNullOrEmpty(token))
                token = TokenFactory.CreateToken("Test User", Array.Empty<string>());

            var client = WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(scheme)
                        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                            scheme, options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);

            return client;
        }
        protected override void Dispose(bool disposing)
        {
            checkpoint.Reset(_configuration["Data:DefaultConnection:ConnectionString"]).GetAwaiter().GetResult();

            base.Dispose(disposing);
        }

    }
}
