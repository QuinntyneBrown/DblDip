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
using static DblDip.Api.FunctionalTests.PhotoGalleriesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class PhotoGalleriesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public PhotoGalleriesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreatePhotoGallery()
        {
            var context = _fixture.Context;

            var photoGallery = PhotoGalleryDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { photoGallery }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreatePhotoGallery, stringContent);

            var response = JsonConvert.DeserializeObject<CreatePhotoGallery.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<PhotoGallery>(response.PhotoGallery.PhotoGalleryId);

            Assert.NotEqual(default, response.PhotoGallery.PhotoGalleryId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemovePhotoGallery()
        {
            var photoGallery = PhotoGalleryBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(photoGallery);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(photoGallery.PhotoGalleryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedPhotoGallery = await context.FindAsync<PhotoGallery>(photoGallery.PhotoGalleryId);

            //Assert.NotEqual(default, removedPhotoGallery.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdatePhotoGallery()
        {
            var photoGallery = PhotoGalleryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photoGallery);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { photoGallery = photoGallery.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<PhotoGallery>(photoGallery.PhotoGalleryId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPhotoGalleries()
        {
            var photoGallery = PhotoGalleryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photoGallery);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.photoGalleries);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPhotoGalleries.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.PhotoGalleries.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPhotoGalleryById()
        {
            var photoGallery = PhotoGalleryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(photoGallery);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(photoGallery.PhotoGalleryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPhotoGalleryById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreatePhotoGallery = "api/photo-galleries";
            }

            public static class Put
            {
                public static string Update = "api/photo-galleries";
            }

            public static class Delete
            {
                public static string By(Guid photoGalleryId)
                {
                    return $"api/photo-galleries/{photoGalleryId}";
                }
            }

            public static class Get
            {
                public static string photoGalleries = "api/photo-galleries";
                public static string By(Guid photoGalleryId)
                {
                    return $"api/photo-galleries/{photoGalleryId}";
                }
            }
        }
    }
}
