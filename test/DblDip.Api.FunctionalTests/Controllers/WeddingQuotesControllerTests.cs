using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.WeddingQuotes;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.WeddingQuotesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class WeddingQuotesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public WeddingQuotesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateWeddingQuote()
        {
            var context = _fixture.Context;

            var weddingQuote = WeddingQuoteDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { weddingQuote }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateWeddingQuote, stringContent);

            var response = JsonConvert.DeserializeObject<CreateWeddingQuote.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<WeddingQuote>(response.WeddingQuote.WeddingQuoteId);

            Assert.NotEqual(default, response.WeddingQuote.WeddingQuoteId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveWeddingQuote()
        {
            var weddingQuote = WeddingQuoteBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(weddingQuote);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(weddingQuote.WeddingQuoteId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedWeddingQuote = await context.FindAsync<WeddingQuote>(weddingQuote.WeddingQuoteId);

            //Assert.NotEqual(default, removedWeddingQuote.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateWeddingQuote()
        {
            var weddingQuote = WeddingQuoteBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(weddingQuote);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { weddingQuote = weddingQuote.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<WeddingQuote>(weddingQuote.WeddingQuoteId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetWeddingQuotes()
        {
            var weddingQuote = WeddingQuoteBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(weddingQuote);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.WeddingQuotes);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetWeddingQuotes.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.WeddingQuotes.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetWeddingQuoteById()
        {
            var weddingQuote = WeddingQuoteBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(weddingQuote);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(weddingQuote.WeddingQuoteId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetWeddingQuoteById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateWeddingQuote = "api/wedding-quotes";
            }

            public static class Put
            {
                public static string Update = "api/wedding-quotes";
            }

            public static class Delete
            {
                public static string By(Guid weddingQuoteId)
                {
                    return $"api/wedding-quotes/{weddingQuoteId}";
                }
            }

            public static class Get
            {
                public static string WeddingQuotes = "api/wedding-quotes";
                public static string By(Guid weddingQuoteId)
                {
                    return $"api/wedding-quotes/{weddingQuoteId}";
                }
            }
        }
    }
}
