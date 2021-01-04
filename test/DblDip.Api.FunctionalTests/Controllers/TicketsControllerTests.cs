using Newtonsoft.Json;
using DblDip.Core.Models;
using DblDip.Domain.Features;
using DblDip.Domain.Features.Tickets;
using DblDip.Testing;
using DblDip.Testing.Builders;
using DblDip.Testing.Builders;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;
using static DblDip.Api.FunctionalTests.TicketsControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class TicketsControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public TicketsControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateTicket()
        {
            var context = _fixture.Context;

            var ticket = TicketDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { ticket }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateTicket, stringContent);

            var response = JsonConvert.DeserializeObject<CreateTicket.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Ticket>(response.Ticket.TicketId);

            Assert.NotEqual(default, response.Ticket.TicketId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveTicket()
        {
            var ticket = TicketBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Store(ticket);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(ticket.TicketId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedTicket = await context.FindAsync<Ticket>(ticket.TicketId);

            Assert.NotEqual(default, removedTicket.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateTicket()
        {
            var ticket = TicketBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(ticket);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { ticket = ticket.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Ticket>(ticket.TicketId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTickets()
        {
            var ticket = TicketBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(ticket);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Tickets);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTickets.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Tickets.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetTicketById()
        {
            var ticket = TicketBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Store(ticket);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(ticket.TicketId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetTicketById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateTicket = "api/tickets";
            }

            public static class Put
            {
                public static string Update = "api/tickets";
            }

            public static class Delete
            {
                public static string By(Guid ticketId)
                {
                    return $"api/tickets/{ticketId}";
                }
            }

            public static class Get
            {
                public static string Tickets = "api/tickets";
                public static string By(Guid ticketId)
                {
                    return $"api/tickets/{ticketId}";
                }
            }
        }
    }
}
