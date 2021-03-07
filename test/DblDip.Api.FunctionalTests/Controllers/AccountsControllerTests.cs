using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Factories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.AccountsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
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

            context.Add(account);

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

            var account = new AccountBuilder(profile.ProfileId, user.UserId).Build();

            profile.UpdateAccountId(account.AccountId);

            var context = _fixture.Context;

            context.Add(user);

            context.Add(profile);

            context.Add(account);

            await context.SaveChangesAsync(default);

            var token = TokenFactory.CreateToken(user, account, new List<Role>());

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { profileId = profile.ProfileId }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient(token).PutAsync(Put.Current, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Account>(account.AccountId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_SetDefaultProfile()
        {
            var user = UserBuilder.WithDefaults(default);

            var profile = ProfileBuilder.WithDefaults();

            var account = new AccountBuilder(profile.ProfileId, user.UserId).Build();

            profile.UpdateAccountId(account.AccountId);

            var context = _fixture.Context;

            context.Add(user);

            context.Add(profile);

            context.Add(account);

            await context.SaveChangesAsync(default);

            var token = TokenFactory.CreateToken(user, default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { profileId = profile.ProfileId }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient(token).PutAsync(Put.Default, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Account>(account.AccountId);

            Assert.Equal(profile.ProfileId, account.DefaultProfileId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetCurrentAccountProfiles()
        {
            var user = UserBuilder.WithDefaults(default);

            var profile = ProfileBuilder.WithDefaults();

            var account = new AccountBuilder(profile.ProfileId, user.UserId).Build();

            profile.UpdateAccountId(account.AccountId);

            var context = _fixture.Context;

            context.Add(user);

            context.Add(profile);

            context.Add(account);

            await context.SaveChangesAsync(default);

            var token = TokenFactory.CreateToken(user, default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient(token).GetAsync(Get.Profiles);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetCurrentAccountProfiles.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.Single(response.Profiles);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateAccount()
        {
            var account = AccountBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(account);

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

            context.Add(account);

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

            context.Add(account);

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
                public static string Current = "api/accounts/current-profile";
                public static string Default = "api/accounts/default-profile";
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
                public static string Profiles = "api/accounts/current/profiles";
                public static string By(Guid accountId)
                {
                    return $"api/accounts/{accountId}";
                }
            }
        }
    }
}
