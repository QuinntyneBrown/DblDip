using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.PhotoStudios;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.PhotoStudiosControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class PhotoStudiosControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public PhotoStudiosControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreatePhotoStudio()
        {
            var context = _fixture.Context;

            var photoStudio = PhotoStudioDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { photoStudio }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreatePhotoStudio, stringContent);

            var response = JsonConvert.DeserializeObject<CreatePhotoStudio.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<PhotoStudio>(response.PhotoStudio.PhotoStudioId);

            Assert.NotEqual(default, response.PhotoStudio.PhotoStudioId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemovePhotoStudio()
        {
            var photoStudio = PhotoStudioBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(photoStudio);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(photoStudio.PhotoStudioId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedPhotoStudio = await context.FindAsync<PhotoStudio>(photoStudio.PhotoStudioId);

            //Assert.NotEqual(default, removedPhotoStudio.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdatePhotoStudio()
        {
            var photoStudio = PhotoStudioBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photoStudio);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { photoStudio = photoStudio.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<PhotoStudio>(photoStudio.PhotoStudioId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPhotoStudios()
        {
            var photoStudio = PhotoStudioBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photoStudio);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.photoStudios);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPhotoStudios.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.PhotoStudios.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPhotoStudioById()
        {
            var photoStudio = PhotoStudioBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photoStudio);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(photoStudio.PhotoStudioId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPhotoStudioById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreatePhotoStudio = "api/photo-studios";
            }

            public static class Put
            {
                public static string Update = "api/photo-studios";
            }

            public static class Delete
            {
                public static string By(Guid photoStudioId)
                {
                    return $"api/photo-studios/{photoStudioId}";
                }
            }

            public static class Get
            {
                public static string photoStudios = "api/photo-studios";
                public static string By(Guid photoStudioId)
                {
                    return $"api/photo-studios/{photoStudioId}";
                }
            }
        }
    }
}
