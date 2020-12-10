using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Feedbacks;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.FeedbacksControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class FeedbacksControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public FeedbacksControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateFeedback()
        {
            var context = _fixture.Context;

            var feedback = FeedbackDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { feedback }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateFeedback, stringContent);

            var response = JsonConvert.DeserializeObject<CreateFeedback.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Feedback>(response.Feedback.FeedbackId);

            Assert.NotEqual(default, response.Feedback.FeedbackId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveFeedback()
        {
            var feedback = FeedbackBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(feedback);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(feedback.FeedbackId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedFeedback = await context.FindAsync<Feedback>(feedback.FeedbackId);

            Assert.NotEqual(default, removedFeedback.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateFeedback()
        {
            var feedback = FeedbackBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(feedback);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { feedback = feedback.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Feedback>(feedback.FeedbackId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetFeedbacks()
        {
            var feedback = FeedbackBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(feedback);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.feedbacks);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetFeedbacks.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Feedbacks.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetFeedbackById()
        {
            var feedback = FeedbackBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(feedback);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(feedback.FeedbackId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetFeedbackById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateFeedback = "api/feedbacks";
            }

            public static class Put
            {
                public static string Update = "api/feedbacks";
            }

            public static class Delete
            {
                public static string By(Guid feedbackId)
                {
                    return $"api/feedbacks/{feedbackId}";
                }
            }

            public static class Get
            {
                public static string feedbacks = "api/feedbacks";
                public static string By(Guid feedbackId)
                {
                    return $"api/feedbacks/{feedbackId}";
                }
            }
        }
    }
}
