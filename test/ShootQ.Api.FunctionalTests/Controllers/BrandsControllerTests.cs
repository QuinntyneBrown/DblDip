using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Brands;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.BrandsControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class BrandsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public BrandsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateBrand()
        {
            var context = _fixture.Context;

            var brand = BrandDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { brand }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateBrand, stringContent);

            var response = JsonConvert.DeserializeObject<CreateBrand.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Brand>(response.Brand.BrandId);

            Assert.NotEqual(default, response.Brand.BrandId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveBrand()
        {
            var brand = BrandBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(brand);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(brand.BrandId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedBrand = await context.FindAsync<Brand>(brand.BrandId);

            Assert.NotEqual(default, removedBrand.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateBrand()
        {
            var brand = BrandBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(brand);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { brand = brand.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Brand>(brand.BrandId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetBrands()
        {
            var brand = BrandBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(brand);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Brands);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetBrands.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Brands.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetBrandById()
        {
            var brand = BrandBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(brand);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(brand.BrandId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetBrandById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateBrand = "api/brands";
            }

            public static class Put
            {
                public static string Update = "api/brands";
            }

            public static class Delete
            {
                public static string By(Guid brandId)
                {
                    return $"api/brands/{brandId}";
                }
            }

            public static class Get
            {
                public static string Brands = "api/brands";
                public static string By(Guid brandId)
                {
                    return $"api/brands/{brandId}";
                }
            }
        }
    }
}
