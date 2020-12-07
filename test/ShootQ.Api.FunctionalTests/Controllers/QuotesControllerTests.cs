using BuildingBlocks.Core;
using ShootQ.Core.ValueObjects;
using ShootQ.Domain.Features.Quotes;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using Xunit;
using Xunit.Abstractions;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class QuotesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        private ITestOutputHelper _testOutputHelper;
        public QuotesControllerTests(ApiTestFixture fixture, ITestOutputHelper testOutputHelper)
        {
            _fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_QuoteWedding()
        {
            var photographyRate = _fixture.Context.Store(PhotographyRateBuilder.WithDefaults());

            var wedding = _fixture.Context.Store(WeddingBuilder.WithDefaults(photographyRate));

            await _fixture.Context.SaveChangesAsync(default);

            var client = _fixture.CreateClient();

            var response = await client.PostAsAsync<dynamic, CreateWeddingQuote.Response>(Endpoints.Post.CreateWeddingQuote, new
            {
                WeddingId = wedding.WeddingId,
                Email = "quinntynebrown@gmail.com"
            });

            _testOutputHelper.WriteLine($"{response.Quote.Total.Value}");

            Assert.Equal((Price)500, response.Quote.Total);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateWeddingQuote = "api/quotes/wedding";
            }
        }
    }
}
