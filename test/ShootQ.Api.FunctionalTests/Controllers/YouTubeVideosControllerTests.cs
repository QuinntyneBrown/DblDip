using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.YouTubeVideos;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.YouTubeVideosControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class YouTubeVideosControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public YouTubeVideosControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateYouTubeVideo()
        {
            var context = _fixture.Context;

            var youTubeVideo = YouTubeVideoDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { youTubeVideo }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateYouTubeVideo, stringContent);

            var response = JsonConvert.DeserializeObject<CreateYouTubeVideo.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<YouTubeVideo>(response.YouTubeVideo.YouTubeVideoId);

            Assert.NotEqual(default, response.YouTubeVideo.YouTubeVideoId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveYouTubeVideo()
        {
            var youTubeVideo = YouTubeVideoBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(youTubeVideo);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(youTubeVideo.YouTubeVideoId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedYouTubeVideo = await context.FindAsync<YouTubeVideo>(youTubeVideo.YouTubeVideoId);

            Assert.NotEqual(default, removedYouTubeVideo.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateYouTubeVideo()
        {
            var youTubeVideo = YouTubeVideoBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(youTubeVideo);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { youTubeVideo = youTubeVideo.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<YouTubeVideo>(youTubeVideo.YouTubeVideoId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetYouTubeVideos()
        {
            var youTubeVideo = YouTubeVideoBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(youTubeVideo);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.YouTubeVideos);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetYouTubeVideos.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.YouTubeVideos.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetYouTubeVideoById()
        {
            var youTubeVideo = YouTubeVideoBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(youTubeVideo);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(youTubeVideo.YouTubeVideoId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetYouTubeVideoById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateYouTubeVideo = "api/you-tube-videos";
            }

            public static class Put
            {
                public static string Update = "api/you-tube-videos";
            }

            public static class Delete
            {
                public static string By(Guid youTubeVideoId)
                {
                    return $"api/you-tube-videos/{youTubeVideoId}";
                }
            }

            public static class Get
            {
                public static string YouTubeVideos = "api/you-tube-videos";
                public static string By(Guid youTubeVideoId)
                {
                    return $"api/you-tube-videos/{youTubeVideoId}";
                }
            }
        }
    }
}
