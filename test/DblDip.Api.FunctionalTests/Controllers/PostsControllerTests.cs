using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Posts;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.PostsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class PostsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public PostsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreatePost()
        {
            var context = _fixture.Context;

            var post = PostDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { post }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreatePost, stringContent);

            var response = JsonConvert.DeserializeObject<CreatePost.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<DblDip.Core.Models.Post>(response.Post.PostId);

            Assert.NotEqual(default, response.Post.PostId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemovePost()
        {
            var post = PostBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(post);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(post.PostId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedPost = await context.FindAsync<DblDip.Core.Models.Post>(post.PostId);

            Assert.NotEqual(default, removedPost.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdatePost()
        {
            var post = PostBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(post);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { post = post.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<DblDip.Core.Models.Post>(post.PostId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPosts()
        {
            var post = PostBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(post);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Posts);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPosts.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Posts.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPostById()
        {
            var post = PostBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(post);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(post.PostId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPostById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreatePost = "api/posts";
            }

            public static class Put
            {
                public static string Update = "api/posts";
            }

            public static class Delete
            {
                public static string By(Guid postId)
                {
                    return $"api/posts/{postId}";
                }
            }

            public static class Get
            {
                public static string Posts = "api/posts";
                public static string By(Guid postId)
                {
                    return $"api/posts/{postId}";
                }
            }
        }
    }
}
