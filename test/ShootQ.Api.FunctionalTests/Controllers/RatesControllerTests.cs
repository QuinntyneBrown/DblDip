using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Rates;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.RatesControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class RatesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public RatesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateRate()
        {
            var context = _fixture.Context;

            var rate = RateDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { rate }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateRate, stringContent);

            var response = JsonConvert.DeserializeObject<CreateRate.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Rate>(response.Rate.RateId);

            Assert.NotEqual(default, response.Rate.RateId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveRate()
        {
            var rate = RateBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(rate);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(rate.RateId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedRate = await context.FindAsync<Rate>(rate.RateId);

            Assert.NotEqual(default, removedRate.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateRate()
        {
            var rate = RateBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(rate);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { rate = rate.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Rate>(rate.RateId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetRates()
        {
            var rate = RateBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(rate);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.rates);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetRates.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Rates.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetRateById()
        {
            var rate = RateBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(rate);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(rate.RateId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetRateById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateRate = "api/rates";
            }

            public static class Put
            {
                public static string Update = "api/rates";
            }

            public static class Delete
            {
                public static string By(Guid rateId)
                {
                    return $"api/rates/{rateId}";
                }
            }

            public static class Get
            {
                public static string rates = "api/rates";
                public static string By(Guid rateId)
                {
                    return $"api/rates/{rateId}";
                }
            }
        }
    }
}
