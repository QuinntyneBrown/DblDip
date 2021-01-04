using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.ShotLists;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.ShotListsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class ShotListsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ShotListsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateShotList()
        {
            var context = _fixture.Context;

            var shotList = ShotListDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { shotList }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateShotList, stringContent);

            var response = JsonConvert.DeserializeObject<CreateShotList.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<ShotList>(response.ShotList.ShotListId);

            Assert.NotEqual(default, response.ShotList.ShotListId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveShotList()
        {
            var shotList = ShotListBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(shotList);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(shotList.ShotListId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedShotList = await context.FindAsync<ShotList>(shotList.ShotListId);

            Assert.NotEqual(default, removedShotList.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateShotList()
        {
            var shotList = ShotListBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(shotList);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { shotList = shotList.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<ShotList>(shotList.ShotListId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetShotLists()
        {
            var shotList = ShotListBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(shotList);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.ShotLists);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetShotLists.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.ShotLists.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetShotListById()
        {
            var shotList = ShotListBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(shotList);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(shotList.ShotListId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetShotListById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateShotList = "api/shot-lists";
            }

            public static class Put
            {
                public static string Update = "api/shot-lists";
            }

            public static class Delete
            {
                public static string By(Guid shotListId)
                {
                    return $"api/shot-lists/{shotListId}";
                }
            }

            public static class Get
            {
                public static string ShotLists = "api/shot-lists";
                public static string By(Guid shotListId)
                {
                    return $"api/shot-lists/{shotListId}";
                }
            }
        }
    }
}
