using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Testing;
using ShootQ.Testing.Builders;
using System;
using Xunit;
using BuildingBlocks.Core;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class CardsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public CardsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_CreateCard()
        {

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveCard()
        {

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string AddCard = "api/cards";
            }

            public static class Delete
            {
                public static string CardBy(Guid cardId)
                {
                    return $"api/cards/{cardId}";
                }
            }
        }
    }
}
