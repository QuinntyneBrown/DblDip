using BuildingBlocks.Abstractions;
using BuildingBlocks.EventStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShootQ.Api;
using ShootQ.Core.Data;
using ShootQ.Testing.AuthenticationHandlers;
using ShootQ.Testing.Factories;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit.Abstractions;

namespace ShootQ.Testing
{
    public class ApiTestFixture : WebApplicationFactory<Startup>, IDisposable
    {
        private IAppDbContext _context;
        private EventStoreDbContext _dbContext;
        private IConfiguration _configuration;
        private ITestOutputHelper _testOutputHelper;
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



                }
            });
        }
        public IAppDbContext Context
        {
            get
            {

                if (_context == null)
                {
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"])
                        .Options;

                    var context = new EventStoreDbContext(options);
                    var dateTime = new MachineDateTime();
                    var eventStore = new EventStore(context, dateTime, new TestCorrelationIdAccessor(_correlationId));
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

        private IDataIntegrityService _dataIntegrityService;
        public IDataIntegrityService DataIntegrityService
        {
            get
            {
                if (_dataIntegrityService == null)
                {
                    var options = new DbContextOptionsBuilder()
                        .UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"])
                        .Options;
                    var dateTime = new MachineDateTime();

                    var context = new EventStoreDbContext(options);

                    _dataIntegrityService = new DataIntegrityService(new AggregateSet(context, dateTime));
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
                .UseSqlServer(_configuration["Data:DefaultConnection:ConnectionString"])
                .Options;

            var context = new EventStoreDbContext(options);

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
