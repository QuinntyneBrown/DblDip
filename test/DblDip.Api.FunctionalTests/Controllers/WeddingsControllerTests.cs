using BuildingBlocks.Core;
using DblDip.Domain.Features.Weddings;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.ValueObjects;
using System;
using Xunit;

namespace DblDip.Api.FunctionalTests.Controllers
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

            var dto = new CreateWedding.Request(1, DateTime.UtcNow, defaultLocation.Longitude, defaultLocation.Latitude);

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
