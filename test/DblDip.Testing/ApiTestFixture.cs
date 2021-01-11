using BuildingBlocks.EventStore;
using DblDip.Api;
using DblDip.Core.Data;
using DblDip.Data;
using DblDip.Testing.AuthenticationHandlers;
using DblDip.Testing.Factories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using static DblDip.Core.Constants.ConfigurationKeys;

namespace DblDip.Testing
{
    public class ApiTestFixture : WebApplicationFactory<Startup>, IDisposable
    {
        private IDblDipDbContext _context;
        private IConfiguration _configuration;
        private readonly Guid _correlationId = Guid.NewGuid();

        public ApiTestFixture()
        {
            _configuration = ConfigurationFactory.Create();
        }

        public new HttpClient CreateClient()
        {
            var client = base.CreateClient();

            client.DefaultRequestHeaders.Add("correlationId", $"{_correlationId}");

            return client;
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

                    var context = scopedServices.GetRequiredService<IDblDipDbContext>();

                    DbInitializer.Initialize(context, ConfigurationFactory.Create());

                }
            });
        }
        public IDblDipDbContext Context
        {
            get
            {
                if (_context == null)
                {
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(_configuration[DataDefaultConnectionString])
                        .Options;

                    var dateTime = new MachineDateTime();

                    _context = new DblDipDbContext(options, dateTime, new TestCorrelationIdAccessor(_correlationId));

                    DbInitializer.Initialize(_context, _configuration);
                }

                return _context;
            }
            set
            {
                _context = value;
            }
        }

        private IDataIntegrityService _dataIntegrityService;
        public IDataIntegrityService DataIntegrityService
        {
            get
            {
                if (_dataIntegrityService == null)
                {
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(_configuration[DataDefaultConnectionString])
                        .Options;
                    var dateTime = new MachineDateTime();

                    

                    _dataIntegrityService = new DataIntegrityService();
                }

                return _dataIntegrityService;
            }
        }


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

            client.DefaultRequestHeaders.Add("correlationId", $"{_correlationId}");

            return client;
        }

        protected override void Dispose(bool disposing)
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(_configuration[DataDefaultConnectionString])
                .Options;

            var context = new DblDipDbContext(options, default, new TestCorrelationIdAccessor(_correlationId));

            foreach (var storedEvent in context.StoredEvents.Where(x => x.CorrelationId == _correlationId))
            {
                context.Remove(storedEvent);
            }

            context.SaveChanges();

            base.Dispose(disposing);
        }
    }

    public class TestCorrelationIdAccessor : ICorrelationIdAccessor
    {
        private Guid _correlationId;
        public TestCorrelationIdAccessor(Guid correlationId)
        {
            _correlationId = correlationId;
        }
        public Guid CorrelationId => _correlationId;
    }
}
