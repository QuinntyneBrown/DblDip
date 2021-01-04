using BuildingBlocks.Core;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using DblDip.Domain.Features;
using DblDip.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xunit;

namespace DblDip.Api.FunctionalTests
{
    public class IdentityControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;

        public IdentityControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetToken()
        {

            var client = _fixture.CreateClient();

            var response = await client.PostAsAsync<dynamic, Authenticate.Response>("api/identity/token", new
            {
                username = "quinntynebrown@gmail.com",
                password = "dbldip"
            });

            Assert.NotNull(response.AccessToken);

        }

        [Fact()]
        public async System.Threading.Tasks.Task Should_SignInByEmailAndQuoteId()
        {
            var client = _fixture.CreateClient();

            var context = _fixture.Context;

            var rate = context.Store(new Rate("test", (Price)1));

            var wedding = context.Store(new Wedding(Location.Create(1, 1).Value, Location.Create(1, 1).Value, Location.Create(1, 1).Value, DateTime.Now, 4));

            var weddingQuote = new WeddingQuote((Email)"random@gmail.com", wedding, rate);

            context.Store(weddingQuote);

            await context.SaveChangesAsync(default);

            var stringContent = new StringContent(JsonConvert.SerializeObject(new { email = weddingQuote.BillToEmail.Value, quoteId = weddingQuote.QuoteId }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/identity/quote", stringContent);

            Assert.NotNull(response);
        }
    }
}
