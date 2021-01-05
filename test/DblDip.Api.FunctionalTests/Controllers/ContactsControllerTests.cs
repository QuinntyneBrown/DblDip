using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Testing;
using DblDip.Testing.Builders;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.ContactsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class ContactsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ContactsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateContact()
        {
            var context = _fixture.Context;

            var contact = ContactDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { contact }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateContact, stringContent);

            var response = JsonConvert.DeserializeObject<CreateContact.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Contact>(response.Contact.ContactId);

            Assert.NotEqual(default, response.Contact.ContactId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveContact()
        {
            var contact = ContactBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(contact);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(contact.ContactId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedContact = await context.FindAsync<Contact>(contact.ContactId);

            Assert.NotEqual(default, removedContact.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateContact()
        {
            var contact = ContactBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(contact);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { contact = contact.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Contact>(contact.ContactId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetContacts()
        {
            var contact = ContactBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(contact);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.contacts);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetContacts.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Contacts.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetContactById()
        {
            var contact = ContactBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(contact);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(contact.ContactId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetContactById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateContact = "api/contacts";
            }

            public static class Put
            {
                public static string Update = "api/contacts";
            }

            public static class Delete
            {
                public static string By(Guid contactId)
                {
                    return $"api/contacts/{contactId}";
                }
            }

            public static class Get
            {
                public static string contacts = "api/contacts";
                public static string By(Guid contactId)
                {
                    return $"api/contacts/{contactId}";
                }
            }
        }
    }
}
