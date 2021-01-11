using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.RolesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class RolesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public RolesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateRole()
        {
            var context = _fixture.Context;

            var role = RoleDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { role }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateRole, stringContent);

            var response = JsonConvert.DeserializeObject<CreateRole.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Role>(response.Role.RoleId);

            Assert.NotEqual(default, response.Role.RoleId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveRole()
        {
            var role = RoleBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Add(role);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(role.RoleId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedRole = await context.FindAsync<Role>(role.RoleId);

            Assert.NotEqual(default, removedRole.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateRole()
        {
            var role = RoleBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(role);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { role = role.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Role>(role.RoleId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetRoles()
        {
            var role = RoleBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(role);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.roles);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetRoles.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Roles.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetRoleById()
        {
            var role = RoleBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(role);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(role.RoleId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetRoleById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateRole = "api/roles";
            }

            public static class Put
            {
                public static string Update = "api/roles";
            }

            public static class Delete
            {
                public static string By(Guid roleId)
                {
                    return $"api/roles/{roleId}";
                }
            }

            public static class Get
            {
                public static string roles = "api/roles";
                public static string By(Guid roleId)
                {
                    return $"api/roles/{roleId}";
                }
            }
        }
    }
}
