using BuildingBlocks.Core;
using ShootQ.Domain.Features.Weddings;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.ValueObjects;
using System;
using Xunit;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class WeddingsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public WeddingsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_CreateWedding()
        {
            var defaultLocation = LocationBuilder.WithDefaults();
            var dto = new CreateWedding.Request
            {
                CustomerId = Guid.NewGuid(),
                DateTime = DateTime.UtcNow,
                Hours = 1,
                PhotographyRateId = Guid.NewGuid(),
                Longitude = defaultLocation.Longitude,
                Latitude = defaultLocation.Latitude
            };

            var client = _fixture.CreateClient();

            var response = await client.PostAsAsync<dynamic, CreateWedding.Response>(Endpoints.Post.CreateWedding, dto);

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateWedding = "api/weddings";
            }
        }
    }
}
