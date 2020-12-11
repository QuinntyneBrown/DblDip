using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Meetings;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.MeetingsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class MeetingsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public MeetingsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateMeeting()
        {
            var context = _fixture.Context;

            var meeting = MeetingDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { meeting }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateMeeting, stringContent);

            var response = JsonConvert.DeserializeObject<CreateMeeting.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Meeting>(response.Meeting.MeetingId);

            Assert.NotEqual(default, response.Meeting.MeetingId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveMeeting()
        {
            var meeting = MeetingBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(meeting);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(meeting.MeetingId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedMeeting = await context.FindAsync<Meeting>(meeting.MeetingId);

            Assert.NotEqual(default, removedMeeting.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateMeeting()
        {
            var meeting = MeetingBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(meeting);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { meeting = meeting.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Meeting>(meeting.MeetingId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetMeetings()
        {
            var meeting = MeetingBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(meeting);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.meetings);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetMeetings.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Meetings.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetMeetingById()
        {
            var meeting = MeetingBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(meeting);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(meeting.MeetingId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetMeetingById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateMeeting = "api/meetings";
            }

            public static class Put
            {
                public static string Update = "api/meetings";
            }

            public static class Delete
            {
                public static string By(Guid meetingId)
                {
                    return $"api/meetings/{meetingId}";
                }
            }

            public static class Get
            {
                public static string meetings = "api/meetings";
                public static string By(Guid meetingId)
                {
                    return $"api/meetings/{meetingId}";
                }
            }
        }
    }
}
