using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Accounts;
using DblDip.Testing;
using DblDip.Testing.Builders.Core.Models;
using DblDip.Testing.Builders.Domain.Dtos;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.Controllers.AccountsControllerTests.Endpoints;
using System.Collections.Generic;
using DblDip.Testing.Builders;

namespace DblDip.Api.FunctionalTests.Controllers
{
    public class AccountsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public AccountsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_CreateAccount()
        {
            var context = _fixture.Context;

            var account = AccountDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { account }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateAccount, stringContent);

            var response = JsonConvert.DeserializeObject<CreateAccount.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Account>(response.Account.AccountId);

            Assert.NotEqual(default, response.Account.AccountId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveAccount()
        {
            var account = AccountBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(account);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(account.AccountId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedAccount = await context.FindAsync<Account>(account.AccountId);

            Assert.NotEqual(default, removedAccount.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_SetCurrentProfile()
        {
            var user = UserBuilder.WithDefaults(default);

            var profile = ProfileBuilder.WithDefaults();

            var account = new AccountBuilder(new List<Guid> { profile.ProfileId }, user.UserId).Build();

            var context = _fixture.Context;

            context.Store(user);

            context.Store(profile);

            context.Store(account);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { profileId = profile.ProfileId }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Current, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Account>(account.AccountId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_SetDefaultProfile()
        {
            var account = AccountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(account);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { account = account.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Account>(account.AccountId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateAccount()
        {
            var account = AccountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(account);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { account = account.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Account>(account.AccountId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetAccounts()
        {
            var account = AccountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(account);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Accounts);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetAccounts.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Accounts.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetAccountById()
        {
            var account = AccountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(account);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(account.AccountId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetAccountById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateAccount = "api/accounts";
            }

            public static class Put
            {
                public static string Update = "api/accounts";
                public static string Current = "api/accounts/profile/current";
                public static string Default = "api/accounts/profile/default";
            }

            public static class Delete
            {
                public static string By(Guid accountId)
                {
                    return $"api/accounts/{accountId}";
                }
            }

            public static class Get
            {
                public static string Accounts = "api/accounts";
                public static string By(Guid accountId)
                {
                    return $"api/accounts/{accountId}";
                }
            }
        }
    }
}
