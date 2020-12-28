using BuildingBlocks.AspNetCore;
using Stripe;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BuildingBlocks.Stripe
{
    public record StripePaymentDto(string Number, long? ExpMonth, int ExpYear, string Cvc, int Value, string Description, string Currency);
    public interface IPaymentProcessor
    {
        Task<bool> ProcessAsync(StripePaymentDto payment);
    }

    public class PaymentException : HttpStatusCodeException
    {
        public PaymentException(string detail)
            : base((int)HttpStatusCode.BadRequest, "Payment", "Payment Issue", detail)
        {

        }
    }

    public class PaymentProcessor : IPaymentProcessor
    {
        public async Task<bool> ProcessAsync(StripePaymentDto payment)
        {
            try
            {
                var optionsToken = new TokenCreateOptions()
                {
                    Card = new CreditCardOptions
                    {
                        Number = payment.Number,
                        ExpYear = payment.ExpYear,
                        ExpMonth = payment.ExpMonth,
                        Cvc = payment.Cvc
                    }
                };

                var serviceToken = new TokenService();
                var stripeToken = await serviceToken.CreateAsync(optionsToken);
                var options = new ChargeCreateOptions
                {
                    Amount = payment.Value,
                    Currency = payment.Currency,
                    Description = payment.Description,
                    Source = stripeToken.Id
                };

                var service = new ChargeService();
                var charge = await service.CreateAsync(options);

                return charge.Paid;
            }
            catch (Exception e)
            {
                throw new PaymentException(e.Message);
            }
        }
    }
}
