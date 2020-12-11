using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Blogs;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.BlogsControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class BlogsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public BlogsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateBlog()
        {
            var context = _fixture.Context;

            var blog = BlogDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { blog }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateBlog, stringContent);

            var response = JsonConvert.DeserializeObject<CreateBlog.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Blog>(response.Blog.BlogId);

            Assert.NotEqual(default, response.Blog.BlogId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveBlog()
        {
            var blog = BlogBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(blog);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(blog.BlogId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedBlog = await context.FindAsync<Blog>(blog.BlogId);

            Assert.NotEqual(default, removedBlog.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateBlog()
        {
            var blog = BlogBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(blog);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { blog = blog.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Blog>(blog.BlogId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetBlogs()
        {
            var blog = BlogBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(blog);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Blogs);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetBlogs.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Blogs.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetBlogById()
        {
            var blog = BlogBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(blog);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(blog.BlogId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetBlogById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateBlog = "api/blogs";
            }

            public static class Put
            {
                public static string Update = "api/blogs";
            }

            public static class Delete
            {
                public static string By(Guid blogId)
                {
                    return $"api/blogs/{blogId}";
                }
            }

            public static class Get
            {
                public static string Blogs = "api/blogs";
                public static string By(Guid blogId)
                {
                    return $"api/blogs/{blogId}";
                }
            }
        }
    }
}
