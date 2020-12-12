using DblDip.Core.Models;
using MediatR;

namespace DblDip.Domain.IntegrationEvents
{
    public record QuoteCreated(Quote Quote): INotification;
}
