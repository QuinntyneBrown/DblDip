using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Points;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.PointsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class PointsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public PointsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreatePoint()
        {
            var context = _fixture.Context;

            var point = PointDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { point }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreatePoint, stringContent);

            var response = JsonConvert.DeserializeObject<CreatePoint.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Point>(response.Point.PointId);

            Assert.NotEqual(default, response.Point.PointId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemovePoint()
        {
            var point = PointBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(point);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(point.PointId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedPoint = await context.FindAsync<Point>(point.PointId);

            Assert.NotEqual(default, removedPoint.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdatePoint()
        {
            var point = PointBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(point);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { point = point.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Point>(point.PointId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPoints()
        {
            var point = PointBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(point);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Points);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPoints.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Points.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetPointById()
        {
            var point = PointBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(point);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(point.PointId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetPointById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreatePoint = "api/points";
            }

            public static class Put
            {
                public static string Update = "api/points";
            }

            public static class Delete
            {
                public static string By(Guid pointId)
                {
                    return $"api/points/{pointId}";
                }
            }

            public static class Get
            {
                public static string Points = "api/points";
                public static string By(Guid pointId)
                {
                    return $"api/points/{pointId}";
                }
            }
        }
    }
}
