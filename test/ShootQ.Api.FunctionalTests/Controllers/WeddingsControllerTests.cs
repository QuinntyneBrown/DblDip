using ShootQ.Domain.Features.Weddings;
using ShootQ.Testing;
using System;
using Xunit;
using BuildingBlocks.Core;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class WeddingsControllerTests: IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public WeddingsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_CreateWedding()
        {
            var dto = new CreateWedding.Request
            {
                CustomerId = Guid.NewGuid(),
                DateTime = DateTime.UtcNow,
                Hours = 1,
                PhotographyRateId = Guid.NewGuid()
            };

            var client = _fixture.CreateAuthenticatedClient();
            var response = await client.PostAsAsync<dynamic, CreateWedding.Response>(Endpoints.Post.CreateWedding, dto);

            Assert.NotNull(response);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_QuoteWedding()
        {
            var photographyRate = _fixture.Context.Store(PhotographyRateBuilder.WithDefaults());

            var wedding = _fixture.Context.Store(WeddingBuilder.WithDefaults(photographyRate));

            await _fixture.Context.SaveChangesAsync(default);

            var client = _fixture.CreateAuthenticatedClient();

            var response = await client.PostAsAsync<dynamic, QuoteWedding.Response>($"api/weddings/{wedding.WeddingId}/quote", null);

            Assert.Equal((Price)5, response.Total);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateWedding = "api/weddings";

                public static string QuoteBy(Guid weddingId)
                {
                    return $"api/weddings/{weddingId}/quote";
                }
            }
        }
    }
}
