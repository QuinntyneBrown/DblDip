using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.SystemAdministrators;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.SystemAdministratorsControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class SystemAdministratorsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public SystemAdministratorsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateSystemAdministrator()
        {
            var context = _fixture.Context;

            var systemAdministrator = SystemAdministratorDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { systemAdministrator }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateSystemAdministrator, stringContent);

            var response = JsonConvert.DeserializeObject<CreateSystemAdministrator.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<SystemAdministrator>(response.SystemAdministrator.SystemAdministratorId);

            Assert.NotEqual(default, response.SystemAdministrator.SystemAdministratorId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveSystemAdministrator()
        {
            var systemAdministrator = SystemAdministratorBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(systemAdministrator);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(systemAdministrator.SystemAdministratorId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedSystemAdministrator = await context.FindAsync<SystemAdministrator>(systemAdministrator.SystemAdministratorId);

            Assert.NotEqual(default, removedSystemAdministrator.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateSystemAdministrator()
        {
            var systemAdministrator = SystemAdministratorBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(systemAdministrator);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { systemAdministrator = systemAdministrator.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<SystemAdministrator>(systemAdministrator.SystemAdministratorId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetSystemAdministrators()
        {
            var systemAdministrator = SystemAdministratorBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(systemAdministrator);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.SystemAdministrators);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetSystemAdministrators.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.SystemAdministrators.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetSystemAdministratorById()
        {
            var systemAdministrator = SystemAdministratorBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(systemAdministrator);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(systemAdministrator.SystemAdministratorId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetSystemAdministratorById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateSystemAdministrator = "api/system-administrators";
            }

            public static class Put
            {
                public static string Update = "api/system-administrators";
            }

            public static class Delete
            {
                public static string By(Guid systemAdministratorId)
                {
                    return $"api/system-administrators/{systemAdministratorId}";
                }
            }

            public static class Get
            {
                public static string SystemAdministrators = "api/system-administrators";
                public static string By(Guid systemAdministratorId)
                {
                    return $"api/system-administrators/{systemAdministratorId}";
                }
            }
        }
    }
}
