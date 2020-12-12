using BuildingBlocks.Core;
using DblDip.Core.ValueObjects;
using DblDip.Domain.Features.Quotes;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using Xunit;
using Xunit.Abstractions;

namespace DblDip.Api.FunctionalTests.Controllers
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
            var expectedPrice = (Price)500;

            var context = _fixture.Context;

            var photographyRate = context.Store(PhotographyRateBuilder.WithDefaults());

            var wedding = WeddingBuilder.WithDefaults(photographyRate);

            context.Store(wedding);

            await context.SaveChangesAsync(default);

            var client = _fixture.CreateClient();

            var response = await client.PostAsAsync<dynamic, CreateWeddingQuote.Response>(Endpoints.Post.CreateWeddingQuote, new
            {
                wedding.WeddingId,
                Email = "quinntynebrown@gmail.com"
            });

            _testOutputHelper.WriteLine($"{response.Quote.Total.Value}");

            Assert.Equal(expectedPrice, response.Quote.Total);
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
