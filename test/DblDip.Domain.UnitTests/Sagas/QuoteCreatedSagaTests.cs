using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using DblDip.Domain.IntegrationEvents;
using DblDip.Domain.Sagas;
using DblDip.Testing;
using DblDip.Testing.Builders;
using System.Linq;
using Xunit;

namespace DblDip.Domain.UnitTests.Sagas
{
    public class QuoteCreatedSagaTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Should_CreateALeadProfieUponCreationOfQuoteGivenUserDoesntExist()
        {
            var email = (Email)"test@test.com";

            var rate = RateBuilder.WithDefaults();

            var wedding = WeddingBuilder.WithDefaults(rate);

            var quote = new WeddingQuote(email, wedding, rate);

            var context = new AppDbContextBuilder()
                .Add(rate)
                .Add(wedding)
                .Add(quote)
                .SaveChanges()
                .Build();

            var sut = new QuoteCreatedSaga(context);

            await sut.Handle(new QuoteCreated(quote), default);

            var user = context.Set<User>().Where(x => x.Username == email).Single();
        }
    }
}
