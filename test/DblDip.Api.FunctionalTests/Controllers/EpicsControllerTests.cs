using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Epics;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.EpicsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class EpicsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public EpicsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateEpic()
        {
            var context = _fixture.Context;

            var epic = EpicDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { epic }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateEpic, stringContent);

            var response = JsonConvert.DeserializeObject<CreateEpic.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Epic>(response.Epic.EpicId);

            Assert.NotEqual(default, response.Epic.EpicId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveEpic()
        {
            var epic = EpicBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(epic);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(epic.EpicId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedEpic = await context.FindAsync<Epic>(epic.EpicId);

            Assert.NotEqual(default, removedEpic.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateEpic()
        {
            var epic = EpicBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(epic);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { epic = epic.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Epic>(epic.EpicId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEpics()
        {
            var epic = EpicBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(epic);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Epics);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEpics.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Epics.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEpicById()
        {
            var epic = EpicBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(epic);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(epic.EpicId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEpicById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateEpic = "api/epics";
            }

            public static class Put
            {
                public static string Update = "api/epics";
            }

            public static class Delete
            {
                public static string By(Guid epicId)
                {
                    return $"api/epics/{epicId}";
                }
            }

            public static class Get
            {
                public static string Epics = "api/epics";
                public static string By(Guid epicId)
                {
                    return $"api/epics/{epicId}";
                }
            }
        }
    }
}
