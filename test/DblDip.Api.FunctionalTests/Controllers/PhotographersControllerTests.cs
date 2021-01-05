using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.PhotographersControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class PhotographersControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public PhotographersControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreatePhotographer()
        {
            var context = _fixture.Context;

            var photographer = PhotographerDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { photographer }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreatePhotographer, stringContent);

            var response = JsonConvert.DeserializeObject<CreatePhotographer.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Photographer>(response.Photographer.PhotographerId);

            Assert.NotEqual(default, response.Photographer.PhotographerId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemovePhotographer()
        {
            var photographer = PhotographerBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(photographer);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(photographer.PhotographerId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedPhotographer = await context.FindAsync<Photographer>(photographer.PhotographerId);

            Assert.NotEqual(default, removedPhotographer.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdatePhotographer()
        {
            var photographer = PhotographerBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photographer);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { photographer = photographer.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Photographer>(photographer.PhotographerId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPhotographers()
        {
            var photographer = PhotographerBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photographer);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.photographers);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPhotographers.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Photographers.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPhotographerById()
        {
            var photographer = PhotographerBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photographer);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(photographer.PhotographerId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPhotographerById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreatePhotographer = "api/photographers";
            }

            public static class Put
            {
                public static string Update = "api/photographers";
            }

            public static class Delete
            {
                public static string By(Guid photographerId)
                {
                    return $"api/photographers/{photographerId}";
                }
            }

            public static class Get
            {
                public static string photographers = "api/photographers";
                public static string By(Guid photographerId)
                {
                    return $"api/photographers/{photographerId}";
                }
            }
        }
    }
}
