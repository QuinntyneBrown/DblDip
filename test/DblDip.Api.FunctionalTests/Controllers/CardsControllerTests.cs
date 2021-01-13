using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.CardsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
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
            var context = _fixture.Context;

            var card = CardDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { card }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateCard, stringContent);

            var response = JsonConvert.DeserializeObject<CreateCard.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = await context.FindAsync<Card>(response.Card.CardId);

            Assert.NotEqual(default, sut.CardId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveCard()
        {
            var card = CardBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Add(card);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(card.CardId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedCard = await context.FindAsync<Card>(card.CardId);

            Assert.NotEqual(default, removedCard.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateCard()
        {
            var card = CardBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(card);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { card = card.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Card>(card.CardId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetCards()
        {
            var card = CardBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(card);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Cards);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetCards.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Cards.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetCardById()
        {
            var card = CardBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(card);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(card.CardId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetCardById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateCard = "api/cards";
            }

            public static class Put
            {
                public static string Update = "api/cards";
            }

            public static class Delete
            {
                public static string By(Guid cardId)
                {
                    return $"api/cards/{cardId}";
                }
            }

            public static class Get
            {
                public static string Cards = "api/cards";
                public static string By(Guid cardId)
                {
                    return $"api/cards/{cardId}";
                }
            }
        }
    }
}
