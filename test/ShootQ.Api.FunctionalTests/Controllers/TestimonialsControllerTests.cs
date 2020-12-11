using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Testimonials;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.TestimonialsControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class TestimonialsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public TestimonialsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateTestimonial()
        {
            var context = _fixture.Context;

            var testimonial = TestimonialDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { testimonial }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateTestimonial, stringContent);

            var response = JsonConvert.DeserializeObject<CreateTestimonial.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Testimonial>(response.Testimonial.TestimonialId);

            Assert.NotEqual(default, response.Testimonial.TestimonialId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveTestimonial()
        {
            var testimonial = TestimonialBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(testimonial);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(testimonial.TestimonialId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedTestimonial = await context.FindAsync<Testimonial>(testimonial.TestimonialId);

            Assert.NotEqual(default, removedTestimonial.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateTestimonial()
        {
            var testimonial = TestimonialBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(testimonial);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { testimonial = testimonial.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Testimonial>(testimonial.TestimonialId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTestimonials()
        {
            var testimonial = TestimonialBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(testimonial);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Testimonials);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTestimonials.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Testimonials.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTestimonialById()
        {
            var testimonial = TestimonialBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(testimonial);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(testimonial.TestimonialId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTestimonialById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateTestimonial = "api/testimonials";
            }

            public static class Put
            {
                public static string Update = "api/testimonials";
            }

            public static class Delete
            {
                public static string By(Guid testimonialId)
                {
                    return $"api/testimonials/{testimonialId}";
                }
            }

            public static class Get
            {
                public static string Testimonials = "api/testimonials";
                public static string By(Guid testimonialId)
                {
                    return $"api/testimonials/{testimonialId}";
                }
            }
        }
    }
}
