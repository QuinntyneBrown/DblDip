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
using static DblDip.Api.FunctionalTests.EquipmentControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class EquipmentControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public EquipmentControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateEquipment()
        {
            var context = _fixture.Context;

            var equipment = EquipmentDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { equipment }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateEquipment, stringContent);

            var response = JsonConvert.DeserializeObject<CreateEquipment.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Equipment>(response.Equipment.EquipmentId);

            Assert.NotEqual(default, response.Equipment.EquipmentId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveEquipment()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Add(equipment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(equipment.EquipmentId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedEquipment = await context.FindAsync<Equipment>(equipment.EquipmentId);

            Assert.NotEqual(default, removedEquipment.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateEquipment()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(equipment);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { equipment = equipment.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Equipment>(equipment.EquipmentId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEquipment()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(equipment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.equipment);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEquipment.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Equipment.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetEquipmentById()
        {
            var equipment = EquipmentBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(equipment);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(equipment.EquipmentId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetEquipmentById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateEquipment = "api/equipment";
            }

            public static class Put
            {
                public static string Update = "api/equipment";
            }

            public static class Delete
            {
                public static string By(Guid equipmentId)
                {
                    return $"api/equipment/{equipmentId}";
                }
            }

            public static class Get
            {
                public static string equipment = "api/equipment";
                public static string By(Guid equipmentId)
                {
                    return $"api/equipment/{equipmentId}";
                }
            }
        }
    }
}
