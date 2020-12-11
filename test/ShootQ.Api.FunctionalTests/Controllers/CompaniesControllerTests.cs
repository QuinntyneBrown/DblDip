using Newtonsoft.Json;
using ShootQ.Core.Models;
using ShootQ.Domain.Features;
using ShootQ.Domain.Features.Companies;
using ShootQ.Testing;
using ShootQ.Testing.Builders.Core.Models;
using ShootQ.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static ShootQ.Api.FunctionalTests.Controllers.CompaniesControllerTests.Endpoints;

namespace ShootQ.Api.FunctionalTests.Controllers
{
    public class CompaniesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public CompaniesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateCompany()
        {
            var context = _fixture.Context;

            var company = CompanyDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { company }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Post.CreateCompany, stringContent);

            var response = JsonConvert.DeserializeObject<CreateCompany.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Company>(response.Company.CompanyId);

            Assert.NotEqual(default, response.Company.CompanyId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveCompany()
        {
            var company = CompanyBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(company);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(company.CompanyId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedCompany = await context.FindAsync<Company>(company.CompanyId);

            Assert.NotEqual(default, removedCompany.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateCompany()
        {
            var company = CompanyBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(company);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { company = company.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Company>(company.CompanyId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetCompanies()
        {
            var company = CompanyBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(company);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Companies);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetCompanies.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Companies.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetCompanyById()
        {
            var company = CompanyBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(company);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(company.CompanyId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetCompanyById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateCompany = "api/companies";
            }

            public static class Put
            {
                public static string Update = "api/companies";
            }

            public static class Delete
            {
                public static string By(Guid companyId)
                {
                    return $"api/companies/{companyId}";
                }
            }

            public static class Get
            {
                public static string Companies = "api/companies";
                public static string By(Guid companyId)
                {
                    return $"api/companies/{companyId}";
                }
            }
        }
    }
}
