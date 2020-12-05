using ShootQ.Testing;
using System;
using Xunit;

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

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_QuoteWedding()
        {   

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
