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
using static DblDip.Api.FunctionalTests.ExpensesControllerTests.Endpoints;

namespace DblDip.Api.FunctionalTests
{
    public class ExpensesControllerTests : IClassFixture<ApiTestFixture>
    {
        private readonly ApiTestFixture _fixture;
        public ExpensesControllerTests(ApiTestFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async System.Threading.Tasks.Task Should_CreateExpense()
        {
            var context = _fixture.Context;

            var expense = ExpenseDtoBuilder.WithDefaults();

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { expense }), Encoding.UTF8, "application/json");

            using var client = _fixture.CreateAuthenticatedClient();

            var httpResponseMessage = await client.PostAsync(Endpoints.Post.CreateExpense, stringContent);

            var response = JsonConvert.DeserializeObject<CreateExpense.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            var sut = context.FindAsync<Expense>(response.Expense.ExpenseId);

            Assert.NotEqual(default, response.Expense.ExpenseId);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_RemoveExpense()
        {
            var expense = ExpenseBuilder.WithDefaults();

            var context = _fixture.Context;

            var client = _fixture.CreateAuthenticatedClient();

            context.Add(expense);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await client.DeleteAsync(Delete.By(expense.ExpenseId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var removedExpense = await context.FindAsync<Expense>(expense.ExpenseId);

            Assert.NotEqual(default, removedExpense.Deleted);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_UpdateExpense()
        {
            var expense = ExpenseBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(expense);

            await context.SaveChangesAsync(default);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(new { expense = expense.ToDto() }), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().PutAsync(Put.Update, stringContent);

            httpResponseMessage.EnsureSuccessStatusCode();

            var sut = await context.FindAsync<Expense>(expense.ExpenseId);

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetExpenses()
        {
            var expense = ExpenseBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(expense);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.Expenses);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetExpenses.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.True(response.Expenses.Any());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_GetExpenseById()
        {
            var expense = ExpenseBuilder.WithDefaults();

            var context = _fixture.Context;

            context.Add(expense);

            await context.SaveChangesAsync(default);

            var httpResponseMessage = await _fixture.CreateAuthenticatedClient().GetAsync(Get.By(expense.ExpenseId));

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GetExpenseById.Response>(await httpResponseMessage.Content.ReadAsStringAsync());

            Assert.NotNull(response);

        }

        internal static class Endpoints
        {
            public static class Post
            {
                public static string CreateExpense = "api/expenses";
            }

            public static class Put
            {
                public static string Update = "api/expenses";
            }

            public static class Delete
            {
                public static string By(Guid expenseId)
                {
                    return $"api/expenses/{expenseId}";
                }
            }

            public static class Get
            {
                public static string Expenses = "api/expenses";
                public static string By(Guid expenseId)
                {
                    return $"api/expenses/{expenseId}";
                }
            }
        }
    }
}
