using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.DiscountsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class DiscountsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public DiscountsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateDiscount()
        {
            var context = _fixture.Context;

            var discount = DiscountDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { discount }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateDiscount, stringContent);

            var response = JsonConvert.DeserializeObject<CreateDiscount.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Discount>(response.Discount.DiscountId);

            Assert.NotEqual(default, response.Discount.DiscountId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveDiscount()
        {
            var discount = DiscountBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Add(discount);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(discount.DiscountId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedDiscount = await context.FindAsync<Discount>(discount.DiscountId);

            Assert.NotEqual(default, removedDiscount.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateDiscount()
        {
            var discount = DiscountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(discount);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { discount = discount.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Discount>(discount.DiscountId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetDiscounts()
        {
            var discount = DiscountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(discount);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Discounts);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetDiscounts.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Discounts.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetDiscountById()
        {
            var discount = DiscountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(discount);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(discount.DiscountId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetDiscountById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateDiscount = "api/discounts";
            }

            public static class Put
            {
                public static string Update = "api/discounts";
            }

            public static class Delete
            {
                public static string By(Guid discountId)
                {
                    return $"api/discounts/{discountId}";
                }
            }

            public static class Get
            {
                public static string Discounts = "api/discounts";
                public static string By(Guid discountId)
                {
                    return $"api/discounts/{discountId}";
                }
            }
        }
    }
}
