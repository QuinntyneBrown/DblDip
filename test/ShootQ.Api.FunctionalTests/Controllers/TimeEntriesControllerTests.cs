using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.TimeEntries;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.TimeEntriesControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class TimeEntriesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public TimeEntriesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateTimeEntry()
        {
            var context = _fixture.Context;

            var timeEntry = TimeEntryDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { timeEntry }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateTimeEntry, stringContent);

            var response = JsonConvert.DeserializeObject<CreateTimeEntry.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<TimeEntry>(response.TimeEntry.TimeEntryId);

            Assert.NotEqual(default, response.TimeEntry.TimeEntryId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveTimeEntry()
        {
            var timeEntry = TimeEntryBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(timeEntry);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(timeEntry.TimeEntryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedTimeEntry = await context.FindAsync<TimeEntry>(timeEntry.TimeEntryId);

            Assert.NotEqual(default, removedTimeEntry.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateTimeEntry()
        {
            var timeEntry = TimeEntryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(timeEntry);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { timeEntry = timeEntry.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<TimeEntry>(timeEntry.TimeEntryId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTimeEntries()
        {
            var timeEntry = TimeEntryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(timeEntry);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.TimeEntries);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTimeEntries.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.TimeEntries.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTimeEntryById()
        {
            var timeEntry = TimeEntryBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(timeEntry);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(timeEntry.TimeEntryId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTimeEntryById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateTimeEntry = "api/time-entries";
            }

            public static class Put
            {
                public static string Update = "api/time-entries";
            }

            public static class Delete
            {
                public static string By(Guid timeEntryId)
                {
                    return $"api/time-entries/{timeEntryId}";
                }
            }

            public static class Get
            {
                public static string TimeEntries = "api/time-entries";
                public static string By(Guid timeEntryId)
                {
                    return $"api/time-entries/{timeEntryId}";
                }
            }
        }
    }
}
