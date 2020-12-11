using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.EditedPhotos;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.EditedPhotosControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class EditedPhotosControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public EditedPhotosControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateEditedPhoto()
        {
            var context = _fixture.Context;

            var editedPhoto = EditedPhotoDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { editedPhoto }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateEditedPhoto, stringContent);

            var response = JsonConvert.DeserializeObject<CreateEditedPhoto.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<EditedPhoto>(response.EditedPhoto.EditedPhotoId);

            Assert.NotEqual(default, response.EditedPhoto.EditedPhotoId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveEditedPhoto()
        {
            var editedPhoto = EditedPhotoBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(editedPhoto);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(editedPhoto.EditedPhotoId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedEditedPhoto = await context.FindAsync<EditedPhoto>(editedPhoto.EditedPhotoId);

            Assert.NotEqual(default, removedEditedPhoto.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateEditedPhoto()
        {
            var editedPhoto = EditedPhotoBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(editedPhoto);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { editedPhoto = editedPhoto.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<EditedPhoto>(editedPhoto.EditedPhotoId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEditedPhotos()
        {
            var editedPhoto = EditedPhotoBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(editedPhoto);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.EditedPhotos);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEditedPhotos.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.EditedPhotos.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEditedPhotoById()
        {
            var editedPhoto = EditedPhotoBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(editedPhoto);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(editedPhoto.EditedPhotoId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEditedPhotoById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateEditedPhoto = "api/edited-photos";
            }

            public static class Put
            {
                public static string Update = "api/edited-photos";
            }

            public static class Delete
            {
                public static string By(Guid editedPhotoId)
                {
                    return $"api/edited-photos/{editedPhotoId}";
                }
            }

            public static class Get
            {
                public static string EditedPhotos = "api/edited-photos";
                public static string By(Guid editedPhotoId)
                {
                    return $"api/edited-photos/{editedPhotoId}";
                }
            }
        }
    }
}
